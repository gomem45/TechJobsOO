using Microsoft.AspNetCore.Mvc;
using TechJobs.Data;
using TechJobs.Models;
using TechJobs.ViewModels;

namespace TechJobs.Controllers
{
    public class JobController : Controller
    {

        // Our reference to the data store
        private static JobData jobData;

        static JobController()
        {
            jobData = JobData.GetInstance();
        }

        // The detail display for a given Job at URLs like /Job?id=17
        public IActionResult Index(int id)
        {
            // TODO #1 - get the Job with the given ID and pass it into the view
            
            // Find a single job to display
            Job singleJob = jobData.Find(id);

            // Create instance of single job ViewModel
            // NewJobViewModel mainJob = new NewJobViewModel();

            // Populate properties of single job
            // mainJob.Name = singleJob.Name;
            // mainJob.EmployerID = singleJob.Employer.ID;
            // mainJob.CoreCompetencyID = singleJob.ID;
            // mainJob.LocationID = singleJob.ID;
            // mainJob.PositionTypeID = singleJob.PositionType.ID;

            return View(singleJob);
        }

        public IActionResult New()
        {
            NewJobViewModel newJobViewModel = new NewJobViewModel();
            return View(newJobViewModel);
        }

        [HttpPost]
        public IActionResult New(NewJobViewModel newJobViewModel)
        {
            // TODO #6 - Validate the ViewModel and if valid, create a 
            // new Job and add it to the JobData data store. Then
            // redirect to the Job detail (Index) action/view for the new Job.

            // Job fields must be complete
            if (ModelState.IsValid)
            {
                // Create a new Job object
                Job newJob = new Job
                {
                    Name = newJobViewModel.Name,
                    Employer = jobData.Employers.Find(newJobViewModel.EmployerID),
                    Location = jobData.Locations.Find(newJobViewModel.LocationID),
                    CoreCompetency = jobData.CoreCompetencies.Find(newJobViewModel.CoreCompetencyID),
                    PositionType = jobData.PositionTypes.Find(newJobViewModel.PositionTypeID)
                };

                // Add a new single Job
                jobData.Jobs.Add(newJob);

                // Redirect to single job display page
                return Redirect(string.Format("Index?={0}", newJob.ID));
            }

            return View(newJobViewModel);
        }
    }
}
