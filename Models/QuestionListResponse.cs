using System;
using System.Collections.Generic;

namespace zba.intranet.modules.Models
{
    public class QuestionListResponse
    {
        public List<QuestionDto> QuestionDtos { get; set; }
        public bool HasNext { get; set; }
        public int Index { get; set; }
        public int Size { get; set; }
        public int FullDataCount { get; set; }
        public int Max { get; set; }
        public int Min { get; set; }
    }
}
