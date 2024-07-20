using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Entities.Dtos.Records
{
    public record UserUpdateDto:UserDto
    {
        [Required]
        public int Id { get; init; }
    }
}
