using User.Utils;

namespace User.Models
{
    public class UserModel
    {
	  public string Id { get; set; }

	  public string Role { get; set; }

	  public string Email { get; set; }

	  public string Username { get; set; }

	  public string PhoneNumber { get; set; }

	  public string FirstName { get; set; }

	  public string LastName { get; set; }

	  public string ImageProfile { get; set; }

	  public LanguageEnum Language { get; set; }

	  public GenderEnum Gender { get; set; }

	  public TempleateMode Mode { get; set; }

	  public bool Notification { get; set; }

	  public bool AdminNoti { get; set; }

	  public string PersonalNumber { get; set; }

	  public string Address { get; set; }

	  public int? MunicipalityId { get; set; }

	  public int? LegalEntityId { get; set; }

	  public int? PoliceStationID { get; set; }

    }
}