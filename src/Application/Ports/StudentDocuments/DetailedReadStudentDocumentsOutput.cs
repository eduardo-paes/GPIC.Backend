﻿namespace Application.Ports.StudentDocuments
{
    public class DetailedReadStudentDocumentsOutput : BaseStudentDocumentsOutput
    {
        public DateTime? DeletedAt { get; set; }
    }
}