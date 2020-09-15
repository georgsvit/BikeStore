using BikeStore.Models.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Models.View
{
    public class FilterViewModel
    {
        public FilterViewModel(List<Category> categories, List<int> selectedCategories, 
                               List<Suspension> suspensions, List<int> selectedSuspensions, 
                               List<Sex> sexes, List<int> selectedSexes, 
                               List<AgeGroup> ages, List<int> selectedAges, 
                               List<int> years, List<int> selectedYears, 
                               List<double> wheels, List<double> selectedWheels, 
                               int lowPriceBorder, int highPriceBorder, 
                               string selectedSearchString)
        {
            Categories = categories;
            SelectedCategories = selectedCategories;
            Suspensions = suspensions;
            SelectedSuspensions = selectedSuspensions;
            Sexes = sexes;
            SelectedSexes = selectedSexes;
            Ages = ages;
            SelectedAges = selectedAges;            
            Years = years;
            SelectedYears = selectedYears;
            Wheels = wheels;
            SelectedWheels = selectedWheels;
            LowPriceBorder = lowPriceBorder;
            HighPriceBorder = highPriceBorder;
            SelectedSearchString = selectedSearchString;
        }
        
        // Filter parameters
        public List<Category> Categories { get; private set; }
        public List<int> SelectedCategories { get; private set; }
        //
        public List<Suspension> Suspensions { get; private set; }
        public List<int> SelectedSuspensions { get; private set; }
        //
        public List<Sex> Sexes { get; private set; }
        public List<int> SelectedSexes { get; private set; }
        //
        public List<AgeGroup> Ages { get; private set; }
        public List<int> SelectedAges { get; private set; }
        //
        public List<int> Years { get; private set; }
        public List<int> SelectedYears { get; private set; }
        //
        public List<double> Wheels { get; private set; }
        public List<double> SelectedWheels { get; private set; }
        //
        public int LowPriceBorder { get; private set; }
        public int HighPriceBorder { get; private set; }        
        
        // Search parameter
        public string SelectedSearchString { get; private set; }
    }
}
