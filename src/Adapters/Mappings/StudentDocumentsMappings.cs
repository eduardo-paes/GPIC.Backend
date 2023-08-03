using Adapters.Gateways.StudentDocuments;
using AutoMapper;
using Domain.UseCases.Ports.StudentDocuments;

namespace Adapters.Mappings
{
    public class StudentDocumentsMappings : Profile
    {
        public StudentDocumentsMappings()
        {
            _ = CreateMap<CreateStudentDocumentsInput, CreateStudentDocumentsRequest>().ReverseMap();
            _ = CreateMap<UpdateStudentDocumentsInput, UpdateStudentDocumentsRequest>().ReverseMap();
            _ = CreateMap<ResumedReadStudentDocumentsOutput, ResumedReadStudentDocumentsResponse>().ReverseMap();
            _ = CreateMap<ResumedReadStudentDocumentsOutput, ResumedReadStudentDocumentsResponse>().ReverseMap();
            _ = CreateMap<DetailedReadStudentDocumentsOutput, DetailedReadStudentDocumentsResponse>().ReverseMap();
        }
    }
}