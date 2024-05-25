using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace zba.intranet.modules.Models
{
    public class AddAnswerDto
    {
        public int QuestionId { get; set; }
        public List<SingleAnswerDto> SingleAnswerDto { get; set; }
    }

}