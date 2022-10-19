using System.Collections.Generic;

namespace EmiratesIslamic.Core.Helpers;

public class Result
{
    public bool Succeeded { get; set; }

    public IEnumerable<Error> Errors { get; set; }

    public Result()
    {
        Errors = new List<Error>();
    }
}