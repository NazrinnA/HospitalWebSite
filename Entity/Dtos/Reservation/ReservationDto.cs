
public class ReservationDto
{
    public int? DoctorId { get; set; }
    public int? PositionId { get; set; }
    public string? UserName { get; set; }
    public string? UserEmail { get; set; }
    public DoctorGetDto? getDto { get; set; }
    public List<DoctorGetDto>? docs { get; set; }

}