using AutoMapper;
using Adapters.Gateways.StudentDocuments;
using Domain.Contracts.StudentDocuments;

namespace Adapters.Mappings
{
    public class StudentDocumentsMappings : Profile
    {
        public StudentDocumentsMappings()
        {
            CreateMap<CreateStudentDocumentsInput, CreateStudentDocumentsRequest>().ReverseMap();
            CreateMap<UpdateStudentDocumentsInput, UpdateStudentDocumentsRequest>().ReverseMap();
            CreateMap<ResumedReadStudentDocumentsOutput, ResumedReadStudentDocumentsResponse>().ReverseMap();
            CreateMap<DetailedReadStudentDocumentsOutput, DetailedReadStudentDocumentsResponse>().ReverseMap();
        }
    }
}