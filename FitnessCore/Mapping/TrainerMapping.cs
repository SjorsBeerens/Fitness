using FitnessCore.Models;
using FitnessDAL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCore
{
    internal static class TrainerMapping
    {
        internal static Trainer MapToTrainer(this TrainerDTO dto)
        {
            if (dto == null) return null;
            return new Trainer
            {
                TrainerID = dto.TrainerID,
                Name = dto.Name ?? string.Empty,
                Specialty = dto.Specialty ?? string.Empty,
                Experience = dto.Experience ?? string.Empty,
                Price = dto.Price ?? string.Empty,
                Rating = dto.Rating,
                Schedules = new List<TrainerSchedule>(),
                Bookings = new List<TrainerBooking>()
            };
        }
    }
}
