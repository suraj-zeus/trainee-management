
using Trainee.api.dto;
using Trainee.api.Models;

namespace Trainee.api.Repositories;

public static class TraineeRepository
{
   

    private static List<TraineeModel> trainees = [
         new TraineeModel
        {
            Id = 1,
            FirstName = "Suraj",
            LastName = "Prajapati",
            Email = "suraj@gmail.com",
            TechStack = "Java",
            Status = "Active"
        },
        new TraineeModel
        {
            Id = 2,
            FirstName = "Abhishek",
            LastName = "Revenkar",
            Email = "abhishek@gmail.com",
            TechStack = "Android",
            Status = "Training"
        },
        new TraineeModel
        {
            Id = 3,
            FirstName = "Anand",
            LastName = "Prajapati",
            Email = "anand@gamil.com",
            TechStack = "Kotlin",
            Status = "Training"
        }
    ];


    private static int lastId = 3;

    public static void IncrementLastId()
    {
        lastId += 1;
    }

    public static int GetLastId()
    {
        return lastId;
    }

    public static List<TraineeModel> GetTrainees()
    {
        return trainees;
    }

    public static TraineeModel GetById(int id)
    {

        if(id <= 0) return null;

        TraineeModel trainee = null;
        for (int i = 0; i < trainees.Count; i++)
        {
            if (trainees[i].Id == id)
            {
                trainee = trainees[i];
                break;
            }
        }

        return trainee;
    }



    public static void Add(TraineeModel trainee)
    {
        trainees.Add(trainee);
    }

    public static void DeleteById(TraineeModel traineeModel)
    {
        trainees.Remove(traineeModel);
    }

}