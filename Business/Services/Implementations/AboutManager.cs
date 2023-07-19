﻿using AutoMapper;
using Business.Services.Intefaces;
using Business.Utilites.Constants;
using Core.Utilities;
using Core.Utilities.Exceptions;
using Core.Utilities.Extentions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Implementations
{
    public class AboutManager : IAboutService
    {
        private readonly IAboutRepository _repository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public AboutManager(IAboutRepository repository, IMapper mapper, IWebHostEnvironment env)
        {
            _repository = repository;
            _mapper = mapper;
            _env = env;
        }

        public async Task<bool> CreateAsync(AboutPostDto postDto)
        {
            if (!postDto.formFile.ContentType.Contains("image"))
            {
                return false;
            }
            About about = _mapper.Map<About>(postDto);
            about.Image = postDto.formFile.FileCreate(_env.WebRootPath, "assets/img");
            foreach (var item in postDto.Buttons)
            {
                about.Buttons.Add(item);
            }
            await _repository.CreateAsync(about);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            About about = await _repository.Get(a => a.Id == id, "Buttons");
            if(about == null) { throw new NotFoundException(Messages.NotFound); }
            Helper.FileDelete(_env.WebRootPath, "assets/img", about.Image);
            _repository.Delete(about);
            return true;
        }

        public async Task<AboutGetDto> Get()
        {
            About about = await _repository.Get("Buttons");
            AboutGetDto getDto=_mapper.Map<AboutGetDto>(about);
            return getDto;
        }

        public async Task<bool> UpdateAsync(AboutUpdateDto updateDto)
        {
            About about = await _repository.Get(e => e.Id == updateDto.getDto.Id, "Buttons");
            ; if (about == null) throw new NotFoundException(Messages.NotFound);
            if (updateDto.aboutPost.formFile != null)
            {
                if (!updateDto.aboutPost.formFile.ContentType.Contains("image"))
                {
                    return false;
                }
                string imagename = updateDto.aboutPost.formFile.FileCreate(_env.WebRootPath, "assets/img");
                Helper.FileDelete(_env.WebRootPath, "assets/img", about.Image);
                about.Image = imagename;
            }
            about.Title = updateDto.aboutPost.Title;
            about.Description = updateDto.aboutPost.Description;
            if (updateDto.aboutPost.Buttons != null)
            {
                if (updateDto.aboutPost.Buttons.Count == about.Buttons.Count)
                {
                for (int i = 0; i < updateDto.aboutPost.Buttons.Count; i++)
                {
                    about.Buttons[i] = updateDto.aboutPost.Buttons[i];
                }
                }
                if(updateDto.aboutPost.Buttons.Count< about.Buttons.Count)
                {
                    for (int i = 0; i <about.Buttons.Count; i++)
                    {
                        about.Buttons.Remove(about.Buttons[i]);
                    }
                    foreach (var item in updateDto.aboutPost.Buttons)
                    {
                        about.Buttons.Add(item);
                    }
                }
                if (updateDto.aboutPost.Buttons.Count > about.Buttons.Count)
                {
                    foreach (var item in updateDto.aboutPost.Buttons)
                    {
                        about.Buttons.Add(item);
                    }
                }
            }
            _repository.Update(about);
            return true;
        }
    }
}
