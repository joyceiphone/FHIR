using Microsoft.AspNetCore.Mvc;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using FHIR.Models;

namespace FHIR.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class PatientController: ControllerBase
	{
		private readonly FhirClient _fhirClient;

		public PatientController()
		{
			//test server
			_fhirClient = new FhirClient("https://server.fire.ly");
		}

		[HttpPost]
		public async Task<ActionResult<FHIRPatient>> CreatePatient(FHIRPatient model)
		{
			try
			{
				var patient = model.FormatPatient();
				var createdPatient = await _fhirClient.CreateAsync(patient);

				return Ok(createdPatient);

			}catch(Exception ex)
			{
				return StatusCode(500);
			}
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<FHIRPatient>> GetPatient(string id)
		{
			try
			{
				var patient = await _fhirClient.ReadAsync<Patient>($"Patient/{id}");

				if(patient == null)
				{
					return NotFound();
				} else
				{
					return Ok(patient);
				}
			}catch(Exception ex)
			{
				return StatusCode(500);
			}
		}
	}
}
