using Hl7.Fhir.Model;

namespace FHIR.Models
{
	public class FHIRPatient
	{
		public string Id { get; set; }
		public string GivenName { get; set; }
		public string FamilyName { get; set; }
		public String BirthDate { get; set; }

		public string Gender { get; set; }
		public FHIRPatient(){}

		//copy constructor
		public FHIRPatient(Patient patient)
		{
			Id = patient.Id;
			GivenName = patient.Name[0].GivenElement[0].ToString();
			FamilyName = patient.Name[0].Family;
			BirthDate = patient.BirthDate;
			Gender = patient.Gender?.ToString();
		}

		public Patient FormatPatient()
		{
			return new Patient
			{
				Id = Id,
				Name = new List<HumanName>
				{
					new HumanName {Given =  new[]{ GivenName}, Family = FamilyName  }
				},
				Gender = Gender switch
				{
					"female" => AdministrativeGender.Female,
					"male" => AdministrativeGender.Male,
					_ => null,
				},
				BirthDate = BirthDate,
			};
		}
	}
}
