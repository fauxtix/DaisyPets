﻿namespace DaisyPets.Core.Application.TodoManager
{
    public class ToDoDto
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public int Status { get; set; } = 0; // pending / 1 = Completed
        public int CategoryId { get; set; }
        public string? CategoryDescription { get; set; }

        public int Generated { get; set; } = 0; // false

        public DateTime TodoStartDate
        {
            get
            {
                return DateTime.Parse(StartDate!);
            }
        }
        public DateTime TodoEndDate
        {
            get
            {
                return DateTime.Parse(EndDate!);
            }
        }
        public bool Pending
        {
            get
            {
                return Status == 0 ? false : true;
            }
        }

    }
}
