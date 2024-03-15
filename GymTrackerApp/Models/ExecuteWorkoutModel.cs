﻿using CommunityToolkit.Mvvm.ComponentModel;
using GymTrackerApp.Data.Entity;
using System.Collections.ObjectModel;

namespace GymTrackerApp.Models
{
    public partial class ExecuteWorkoutModel : BaseModel
    {
        [ObservableProperty]
        private string title;

        [ObservableProperty]
        private DateTime? startTime, endTime;

        public ObservableCollection<ExecuteExerciseModel> Exercises { get; } = new();

        public ExecuteWorkoutModel()
        {
            Title = string.Empty;
        }

        public Workout ConvertToEty()
        {
            return new Workout
            {
                Title = Title,
                StartTime = StartTime,
                EndTime = EndTime,
                Exercises = Exercises.Select(x => x.ConvertToEty()).ToList()
            };
        }

        public void RemoveSet(ExecuteSetModel set)
        {
            foreach (var exercise in Exercises)
            {
                if (exercise.WarmupSets.Any(x => x.TemporaryId == set.TemporaryId))
                {
                    exercise.WarmupSets.Remove(set);
                    return;
                }
                if (exercise.WorkSets.Any(x => x.TemporaryId == set.TemporaryId))
                {
                    exercise.WorkSets.Remove(set);
                    return;
                }
            }
            throw new InvalidOperationException("there is no matching set");
        }
    }
}
