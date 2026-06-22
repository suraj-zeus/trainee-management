

namespace Trainee.api.Constants;

public static class RedisCacheKeys
{
    public static string Trainee(int id)
    {
        return $"trainee:{id}";
    }

    public static string Mentor(int id)
    {
        return $"mentor:{id}";
    }

    public static string TaskAssignment(int id)
    {
        return $"task-assignment:{id}";
    }

    public static string Submission(int id)
    {
        return $"submission:{id}";
    }
}