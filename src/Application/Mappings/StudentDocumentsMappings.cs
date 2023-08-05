using AutoMapper;
using Domain.Entities;
using Application.Ports.StudentDocuments;

namespace Domain.Mappings
{
    public class StudentDocumentsMappings : Profile
    {
        public StudentDocumentsMappings()
        {
            _ = CreateMap<StudentDocuments, ResumedReadStudentDocumentsOutput>().ReverseMap();
            _ = CreateMap<StudentDocuments, DetailedReadStudentDocumentsOutput>().ReverseMap();
        }
    }
}