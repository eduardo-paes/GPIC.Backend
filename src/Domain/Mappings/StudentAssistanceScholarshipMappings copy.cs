using AutoMapper;
using Domain.Contracts.StudentDocuments;
using Domain.Entities;

namespace Domain.Mappings
{
    public class StudentDocumentsMappings : Profile
    {
        public StudentDocumentsMappings()
        {
            CreateMap<StudentDocuments, ResumedReadStudentDocumentsOutput>().ReverseMap();
            CreateMap<StudentDocuments, DetailedReadStudentDocumentsOutput>().ReverseMap();
        }
    }
}