using Application.DTO.Acheivements;
using Application.DTO.Goals;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Gamification
{
    public class GamificationPageDto
    {
        public int TotalPoints { get; set; }
        public List<GoalDtoForGamification> CurrentGoals { get; set; }

        //Acheivement and Badges
        public List<StudentAchievementDto> Acheivements { get; set; }  
    }
}