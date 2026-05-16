using AutoMapper;
using BusinessLayer.RequestModel.Accessory;
using BusinessLayer.RequestModel.AccessoryKitTemplate;
using BusinessLayer.RequestModel.Class;
using BusinessLayer.RequestModel.Equipment;
using BusinessLayer.RequestModel.EquipmentType;
using BusinessLayer.RequestModel.Grade;
using BusinessLayer.RequestModel.GradeDesciption;
using BusinessLayer.RequestModel.Kit;
using BusinessLayer.RequestModel.KitAccessory;
using BusinessLayer.RequestModel.KitTemplate;
using BusinessLayer.RequestModel.Lab;
using BusinessLayer.RequestModel.RegistrationSchedule;
using BusinessLayer.RequestModel.Room;
using BusinessLayer.RequestModel.ScheduleType;
using BusinessLayer.RequestModel.Semester;
using BusinessLayer.RequestModel.SemesterSubject;
using BusinessLayer.RequestModel.Slot;
using BusinessLayer.RequestModel.Subject;
using BusinessLayer.RequestModel.Team;
using BusinessLayer.RequestModel.TeamEquipment;
using BusinessLayer.RequestModel.TeamKit;
using BusinessLayer.RequestModel.User;
using BusinessLayer.ResponseModel.Accessory;
using BusinessLayer.ResponseModel.AccessoryKitTemplate;
using BusinessLayer.ResponseModel.Assignment;
using BusinessLayer.ResponseModel.Class;
using BusinessLayer.ResponseModel.Equipment;
using BusinessLayer.ResponseModel.EquipmentType;
using BusinessLayer.ResponseModel.Grade;
using BusinessLayer.ResponseModel.GradeDescription;
using BusinessLayer.ResponseModel.Kit;
using BusinessLayer.ResponseModel.KitAccessory;
using BusinessLayer.ResponseModel.KitTemplate;
using BusinessLayer.ResponseModel.Lab;
using BusinessLayer.ResponseModel.RegistrationSchedule;
using BusinessLayer.ResponseModel.Room;
using BusinessLayer.ResponseModel.Schedule;
using BusinessLayer.ResponseModel.ScheduleType;
using BusinessLayer.ResponseModel.SemesterSubject;
using BusinessLayer.ResponseModel.Slot;
using BusinessLayer.ResponseModel.Subject;
using BusinessLayer.ResponseModel.Team;
using BusinessLayer.ResponseModel.TeamEquipment;
using BusinessLayer.ResponseModel.TeamKit;
using BusinessLayer.ResponseModel.User;
using DataLayer.Entities;
using System;
using System.Linq;

namespace OhmLab_FUHCM_BE.AppStarts
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //User
            CreateMap<RegisterRequestModel, User>().ReverseMap();
            CreateMap<UserResponseModel, User>().ReverseMap();
            CreateMap<RegisterRequestModel, UserResponseModel>().ReverseMap();
            CreateMap<UpdateRequestModel, User>().ReverseMap();

            //Subject
            CreateMap<CreateSubjectRequestModel, Subject>()
                .ForMember(dest => dest.SubjectId, opt => opt.Ignore());
                
            CreateMap<UpdateSubjectRequestModel, Subject>();
            CreateMap<Subject, SubjectResponseModel>();

            //Lab
            CreateMap<CreateLabRequestModel, Lab>()
                .ForMember(dest => dest.LabId, opt => opt.Ignore());
            CreateMap<UpdateLabRequestModel, Lab>();
            CreateMap<Lab, LabResponseModel>();


            //Equipment
            CreateMap<CreateEquipmentRequestModel, Equipment>().ReverseMap();
            CreateMap<UpdateEquipmentRequestModel, Equipment>().ReverseMap();
            CreateMap<Equipment, EquipmentResponseModel>()
                .ForMember(dest => dest.EquipmentTypeName, opt => opt.MapFrom(src => src.EquipmentType.EquipmentTypeName))
                .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src => src.Room.RoomId))
                .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.Room.RoomName))
                .ReverseMap();

            // Assignment (Lịch thực hành, Báo cáo, Điểm)
            CreateMap<Schedule, ScheduleResponseModel>()
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.Class != null ? src.Class.ClassName : null))
                .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Class != null && src.Class.SemesterSubject.Subject != null ? src.Class.SemesterSubject.Subject.SubjectName : null))
                .ForMember(dest => dest.LecturerName, opt => opt.MapFrom(src => src.Class != null && src.Class.Lecturer != null ? src.Class.Lecturer.UserFullName : null))
                .ForMember(dest => dest.SlotName, opt => opt.MapFrom(src => src.Class != null && src.Class.ScheduleType != null && src.Class.ScheduleType.Slot != null ? src.Class.ScheduleType.Slot.SlotName : null))
                .ForMember(dest => dest.SlotStartTime, opt => opt.MapFrom(src => src.Class != null && src.Class.ScheduleType != null && src.Class.ScheduleType.Slot != null ? src.Class.ScheduleType.Slot.SlotStartTime : null))
                .ForMember(dest => dest.SlotEndTime, opt => opt.MapFrom(src => src.Class != null && src.Class.ScheduleType != null && src.Class.ScheduleType.Slot != null ? src.Class.ScheduleType.Slot.SlotEndTime : null))
                .ForMember(dest => dest.ScheduleTypeName, opt => opt.MapFrom(src => src.Class != null && src.Class.ScheduleType != null ? src.Class.ScheduleType.ScheduleTypeName : null))
                .ForMember(dest => dest.ScheduleTypeDow, opt => opt.MapFrom(src => src.Class != null && src.Class.ScheduleType != null ? src.Class.ScheduleType.ScheduleTypeDow : null));

            //Report
            CreateMap<BusinessLayer.ResponseModel.Report.ReportResponseModel, Report>().ReverseMap()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserFullName))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.EquipmentId, opt => opt.MapFrom(src => src.Equipment.EquipmentId))
            .ForMember(dest => dest.EquipmentName, opt => opt.MapFrom(src => src.Equipment.EquipmentName))
            .ForMember(dest => dest.RegistrationScheduleId, opt => opt.MapFrom(src => src.RegistraionScheduleId))
            .ForMember(dest => dest.RegistrationScheduleName, opt => opt.MapFrom(src => src.RegistraionSchedule.RegistraionScheduleName));
            CreateMap<Report, BusinessLayer.RequestModel.Report.CreateReportRequestModel>().ReverseMap();
            CreateMap<Report, BusinessLayer.RequestModel.Report.UpdateReportStatusRequestModel>().ReverseMap();



            //TeamEquipment
            CreateMap<GetAllTeamEquipmentRequestModel, TeamEquipment>().ReverseMap();
            CreateMap<TeamEquipment, TeamEquipmentAllResponseModel>()
                .ForMember(dest => dest.TeamName, opt => opt.MapFrom(src => src.Team.TeamName))
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.Team.Class.ClassName))
                .ForMember(dest => dest.ClassId, opt => opt.MapFrom(src => src.Team.Class.ClassId))
                .ForMember(dest => dest.EquipmentName, opt => opt.MapFrom(src => src.Equipment.EquipmentName))
                .ForMember(dest => dest.EquipmentCode, opt => opt.MapFrom(src => src.Equipment.EquipmentCode))
                .ForMember(dest => dest.EquipmentNumberSerial, opt => opt.MapFrom(src => src.Equipment.EquipmentNumberSerial))
                .ReverseMap();
            CreateMap <GetAllTeamEquipmentRequestModel, TeamEquipmentAllResponseModel>().ReverseMap();
            CreateMap <CreateTeamEquipmentRequestModel, TeamEquipment>().ReverseMap();
            CreateMap <UpdateTeamEquipmentRequestModel, TeamEquipment>().ReverseMap();

            //TeamKit
            CreateMap<GetAllTeamKitRequestModel, TeamEquipment>().ReverseMap();
            CreateMap<TeamKit, TeamKitAllResponseModel>()
                .ForMember(dest => dest.TeamName, opt => opt.MapFrom(src => src.Team.TeamName))
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.Team.Class.ClassName))
                .ForMember(dest => dest.ClassId, opt => opt.MapFrom(src => src.Team.Class.ClassId))
                .ForMember(dest => dest.KitName, opt => opt.MapFrom(src => src.Kit.KitName))
                .ForMember(dest => dest.KitDesription, opt => opt.MapFrom(src => src.Kit.KitDescription))
                .ForMember(dest => dest.KitImgUrl, opt => opt.MapFrom(src => src.Kit.KitUrlImg))
                .ReverseMap();
            CreateMap<GetAllTeamKitRequestModel, TeamKitAllResponseModel>().ReverseMap();
            CreateMap<CreateTeamKitRequestModel, TeamKit>().ReverseMap();
            CreateMap<UpdateTeamKitRequestModel, TeamKit>().ReverseMap();


            //KitTemplate
            CreateMap<CreateKitTemplateRequestModel, KitTemplate>().ReverseMap();
            CreateMap<CreateKitTemplateRequestModel, KitTemplateResponseModel>().ReverseMap();
            CreateMap<KitTemplateResponseModel, KitTemplate>().ReverseMap();
            CreateMap<UpdateKitTemplateRequestModel, KitTemplate>().ReverseMap();

            //Kit
            CreateMap<CreateKitRequestModel, Kit>().ReverseMap();
            CreateMap<UpdateKitRequestModel, Kit>().ReverseMap();
            CreateMap<CreateKitRequestModel, KitResponseModel>().ReverseMap();
            CreateMap<Kit, KitResponseModel>()
                 .ForMember(dest => dest.KitTemplateName, opt => opt.MapFrom(src => src.KitTemplate.KitTemplateName))
                 .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src => src.Room.RoomId))
                 .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.Room.RoomName))
                 .ReverseMap();         

            //EquipmentType
            CreateMap<CreateEquipmentTypeRequestModel, EquipmentType>().ReverseMap();
            CreateMap<CreateEquipmentTypeRequestModel, EquipmentTypeResponseModel>().ReverseMap();
            CreateMap<UpdateEquipmentTypeRequestModel, EquipmentType>().ReverseMap();
            CreateMap<EquipmentType, EquipmentTypeResponseModel>().ReverseMap();

            //Room
            CreateMap<CreateRoomRequestModel, Room>().ReverseMap();
            CreateMap<UpdateRoomRequestModel, Room>().ReverseMap();
            CreateMap<Room, RoomResponseModel>().ReverseMap();

            //Class
            CreateMap<Class, ClassResponseModel>()
                .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.SemesterSubject.Subject != null ? src.SemesterSubject.Subject.SubjectName : null))
                .ForMember(dest => dest.LecturerName, opt => opt.MapFrom(src => src.Lecturer != null ? src.Lecturer.UserFullName : null))
                .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.SemesterSubject.Subject != null ? src.SemesterSubject.Subject.SubjectName : null))
                .ForMember(dest => dest.Subjectid, opt => opt.MapFrom(src => src.SemesterSubject.SubjectId))
                .ForMember(dest => dest.SemesterId, opt => opt.MapFrom(src => src.SemesterSubject.SemesterId))
                .ForMember(dest => dest.SemesterName, opt => opt.MapFrom(src =>
                    src.SemesterSubject.Subject != null &&
                    src.SemesterSubject.Subject.SemesterSubjects != null &&
                    src.SemesterSubject.Subject.SemesterSubjects.Any() &&
                    src.SemesterSubject.Subject.SemesterSubjects.First().Semester != null ? 
                    src.SemesterSubject.Subject.SemesterSubjects.First().Semester.SemesterName : null))
                .ForMember(dest => dest.SemesterStartDate, opt => opt.MapFrom(src =>
                    src.SemesterSubject.Subject != null &&
                    src.SemesterSubject.Subject.SemesterSubjects != null &&
                    src.SemesterSubject.Subject.SemesterSubjects.Any() &&
                    src.SemesterSubject.Subject.SemesterSubjects.First().Semester != null ?
                    src.SemesterSubject.Subject.SemesterSubjects.First().Semester.SemesterStartDate : (DateTime?)null))
                .ForMember(dest => dest.SemesterEndDate, opt => opt.MapFrom(src =>
                    src.SemesterSubject.Subject != null &&
                    src.SemesterSubject.Subject.SemesterSubjects != null &&
                    src.SemesterSubject.Subject.SemesterSubjects.Any() &&
                    src.SemesterSubject.Subject.SemesterSubjects.First().Semester != null ?
                    src.SemesterSubject.Subject.SemesterSubjects.First().Semester.SemesterEndDate : (DateTime?)null))
                .ForMember(dest => dest.ScheduleTypeName, opt => opt.MapFrom(src => src.ScheduleType != null ? src.ScheduleType.ScheduleTypeName : null))
                .ForMember(dest => dest.ScheduleTypeDow, opt => opt.MapFrom(src => src.ScheduleType != null ? src.ScheduleType.ScheduleTypeDow : null))
                .ForMember(dest => dest.SlotName, opt => opt.MapFrom(src => src.ScheduleType != null && src.ScheduleType.Slot != null ? src.ScheduleType.Slot.SlotName : null))
                .ForMember(dest => dest.SlotStartTime, opt => opt.MapFrom(src => src.ScheduleType != null && src.ScheduleType.Slot != null ? src.ScheduleType.Slot.SlotStartTime : null))
                .ForMember(dest => dest.SlotEndTime, opt => opt.MapFrom(src => src.ScheduleType != null && src.ScheduleType.Slot != null ? src.ScheduleType.Slot.SlotEndTime : null))
                .ForMember(dest => dest.ClassUsers, opt => opt.MapFrom(src => src.ClassUsers))
                .ReverseMap();

            CreateMap<ClassResponseModel, Class>()
                .ForMember(dest => dest.ClassId, opt => opt.Ignore());
            CreateMap<CreateClassRequestModel, Class>()
                .ForMember(dest => dest.ClassId, opt => opt.Ignore());

            //Lab
            CreateMap<Lab, LabResponseModel>()
                .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Subject != null ? src.Subject.SubjectName : null))
                .ForMember(dest => dest.SubjectCode,opt => opt.MapFrom(src => src.Subject != null ? src.Subject.SubjectCode : null))
                .ForMember(dest => dest.RequiredEquipments, opt => opt.MapFrom(src => src.LabEquipmentTypes))
                .ForMember(dest => dest.RequiredKits, opt => opt.MapFrom(src => src.LabKitTemplates));

            //Lab Equipment
            CreateMap<LabEquipmentType, LabEquipmentResponseModel>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.LabEquipmentTypeStatus))
                .ForMember(dest => dest.EquipmentTypeName, opt => opt.MapFrom(src => src.EquipmentType != null ? src.EquipmentType.EquipmentTypeName : null))
                .ForMember(dest => dest.EquipmentTypeCode, opt => opt.MapFrom(src => src.EquipmentType != null ? src.EquipmentType.EquipmentTypeCode : null))
                .ForMember(dest => dest.EquipmentTypeDescription, opt => opt.MapFrom(src => src.EquipmentType != null ? src.EquipmentType.EquipmentTypeDescription : null))
                .ForMember(dest => dest.EquipmentTypeQuantity, opt => opt.MapFrom(src => src.EquipmentType != null ? src.EquipmentType.EquipmentTypeQuantity : 0))
                .ForMember(dest => dest.EquipmentTypeUrlImg, opt => opt.MapFrom(src => src.EquipmentType != null ? src.EquipmentType.EquipmentTypeUrlImg : null));

            //Lab Kit
            CreateMap<LabKitTemplate, LabKitResponseModel>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.LabKitTemplateStatus))
                .ForMember(dest => dest.KitTemplateName, opt => opt.MapFrom(src => src.KitTemplate != null ? src.KitTemplate.KitTemplateName : null))
                .ForMember(dest => dest.KitTemplateQuantity, opt => opt.MapFrom(src => src.KitTemplate != null ? src.KitTemplate.KitTemplateQuantity : 0))
                .ForMember(dest => dest.KitTemplateDescription, opt => opt.MapFrom(src => src.KitTemplate != null ? src.KitTemplate.KitTemplateDescription : null))
                .ForMember(dest => dest.KitTemplateUrlImg, opt => opt.MapFrom(src => src.KitTemplate != null ? src.KitTemplate.KitTemplateUrlImg : null))
                .ForMember(dest => dest.KitTemplateStatus, opt => opt.MapFrom(src => src.KitTemplate != null ? src.KitTemplate.KitTemplateStatus : null));

            //classuser
            // ClassUser -> ClassUserResponseModel
            CreateMap<ClassUser, ClassUserResponseModel>()
                .ForMember(dest => dest.ClassUserId, opt => opt.MapFrom(src => src.ClassUserId))
                .ForMember(dest => dest.ClassId, opt => opt.MapFrom(src => src.ClassId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.Class != null ? src.Class.ClassName : null))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? src.User.UserFullName : null))
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User != null ? src.User.UserEmail : null))
                .ForMember(dest => dest.UserRole, opt => opt.MapFrom(src => src.User != null ? src.User.UserRoleName : null))
                .ForMember(dest => dest.UserNumberCode, opt => opt.MapFrom(src => src.User != null ? src.User.UserNumberCode : null))
                .ForMember(dest => dest.ClassUserStatus, opt => opt.MapFrom(src => src.ClassUserStatus))
                // Thêm mapping cho thông tin môn học
                .ForMember(dest => dest.SubjectId, opt => opt.MapFrom(src => src.Class != null && src.Class.SemesterSubject.Subject != null ? src.Class.SemesterSubject.Subject.SubjectId : (int?)null))
                .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Class != null && src.Class.SemesterSubject.Subject != null ? src.Class.SemesterSubject.Subject.SubjectName : null))
                .ForMember(dest => dest.SubjectCode, opt => opt.MapFrom(src => src.Class != null && src.Class.SemesterSubject.Subject != null ? src.Class.SemesterSubject.Subject.SubjectCode : null))
                .ForMember(dest => dest.SubjectDescription, opt => opt.MapFrom(src => src.Class != null && src.Class.SemesterSubject.Subject != null ? src.Class.SemesterSubject.Subject.SubjectDescription : null))
                .ForMember(dest => dest.SubjectStatus, opt => opt.MapFrom(src => src.Class != null && src.Class.SemesterSubject.Subject != null ? src.Class.SemesterSubject.Subject.SubjectStatus : null));
            CreateMap<CreateSlotRequestModel, Slot>()
               .ForMember(dest => dest.SlotId, opt => opt.Ignore());
            CreateMap<Slot, SlotResponseModel>().ReverseMap();
            CreateMap<CreateSlotRequestModel, SlotResponseModel>();
            
            //ScheduleType
            CreateMap<CreateScheduleTypeRequestModel, ScheduleType>()
               .ForMember(dest => dest.ScheduleTypeId, opt => opt.Ignore());
            CreateMap<UpdateScheduleTypeRequestModel, ScheduleType>();
            CreateMap<ScheduleType, ScheduleTypeResponseModel>()
                .ForMember(dest => dest.SlotName, opt => opt.MapFrom(src => src.Slot != null ? src.Slot.SlotName : null))
                .ForMember(dest => dest.SlotStartTime, opt => opt.MapFrom(src => src.Slot != null ? src.Slot.SlotStartTime : null))
                .ForMember(dest => dest.SlotEndTime, opt => opt.MapFrom(src => src.Slot != null ? src.Slot.SlotEndTime : null))
                .ForMember(dest => dest.SlotDescription, opt => opt.MapFrom(src => src.Slot != null ? src.Slot.SlotDescription : null));

            //Schedule
            CreateMap<Schedule, ScheduleResponseAllModel>()
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.Class != null ? src.Class.ClassName : null))
                .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Class != null && src.Class.SemesterSubject.Subject != null ? src.Class.SemesterSubject.Subject.SubjectName : null))
                .ForMember(dest => dest.SubjectId, opt => opt.MapFrom(src => src.Class != null && src.Class.SemesterSubject.Subject != null ? src.Class.SemesterSubject.Subject.SubjectId : 0))
                .ForMember(dest => dest.LecturerName, opt => opt.MapFrom(src => src.Class != null && src.Class.Lecturer != null ? src.Class.Lecturer.UserFullName : null))
                .ForMember(dest => dest.LecturerId, opt => opt.MapFrom(src => src.Class != null && src.Class.Lecturer != null ? src.Class.Lecturer.UserId : Guid.Empty))
                .ForMember(dest => dest.SlotName, opt => opt.MapFrom(src => src.Class != null && src.Class.ScheduleType != null && src.Class.ScheduleType.Slot != null ? src.Class.ScheduleType.Slot.SlotName : null))
                .ForMember(dest => dest.SlotId, opt => opt.MapFrom(src => src.Class != null && src.Class.ScheduleType != null && src.Class.ScheduleType.Slot != null ? src.Class.ScheduleType.Slot.SlotId : 0))
                .ForMember(dest => dest.SlotStartTime, opt => opt.MapFrom(src => src.Class != null && src.Class.ScheduleType != null && src.Class.ScheduleType.Slot != null ? src.Class.ScheduleType.Slot.SlotStartTime : null))
                .ForMember(dest => dest.SlotEndTime, opt => opt.MapFrom(src => src.Class != null && src.Class.ScheduleType != null && src.Class.ScheduleType.Slot != null ? src.Class.ScheduleType.Slot.SlotEndTime : null));

            //Team
            CreateMap<Team, TeamResponseModel>()
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.Class != null ? src.Class.ClassName : null))
                .ForMember(dest => dest.TeamUsers, opt => opt.MapFrom(src => src.TeamUsers));

            //SemesterSubject
            CreateMap<SemesterSubject, SemesterSubjectResponseModel>()
                .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Subject.SubjectName))
                .ForMember(dest => dest.SemesterName, opt => opt.MapFrom(src => src.Semester.SemesterName))
                .ForMember(dest => dest.SemesterStartDate, opt => opt.MapFrom(src => src.Semester.SemesterStartDate))
                .ForMember(dest => dest.SemesterEndDate, opt => opt.MapFrom(src => src.Semester.SemesterEndDate));
            CreateMap<SemesterSubject, CreateSemesterSubjectRequestModel>().ReverseMap();
            CreateMap<SemesterSubject, UpdateSemesterSubjectRequestModel>().ReverseMap();

            //TeamUser
            CreateMap<TeamUser, TeamUserResponseModel>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? src.User.UserFullName : null))
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User != null ? src.User.UserEmail : null))
                .ForMember(dest => dest.UserNumberCode, opt => opt.MapFrom(src => src.User != null ? src.User.UserNumberCode : null));

            //Team Request Models
            CreateMap<CreateTeamRequestModel, Team>()
                .ForMember(dest => dest.TeamId, opt => opt.Ignore());
            CreateMap<UpdateTeamRequestModel, Team>()
                .ForMember(dest => dest.TeamId, opt => opt.Ignore());

            //Registration
            CreateMap<GetAllRegistrationScheduleRequestModel, RegistraionSchedule>().ReverseMap();
            CreateMap<RegistraionSchedule, RegistrationScheduleAllResponseModel>()
                .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src => src.Teaacher.UserFullName))
                .ForMember(dest => dest.TeacherId, opt => opt.MapFrom(src => src.Teaacher.UserId))
                .ForMember(dest => dest.TeacherRollNumber, opt => opt.MapFrom(src => src.Teaacher.UserRollNumber))
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.Class.ClassName))
                .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.Room.RoomName))
                .ForMember(dest => dest.SubjectId, opt => opt.MapFrom(src => src.Lab.Subject.SubjectId))
                .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Lab.Subject.SubjectName))
                .ForMember(dest => dest.LabName, opt => opt.MapFrom(src => src.Lab.LabName))
                .ForMember(dest => dest.SlotName, opt => opt.MapFrom(src => src.Slot.SlotName))
                .ForMember(dest => dest.SlotStartTime, opt => opt.MapFrom(src => src.Slot.SlotStartTime))
                .ForMember(dest => dest.SlotEndTime, opt => opt.MapFrom(src => src.Slot.SlotEndTime))
                .ReverseMap();
            CreateMap<GetAllRegistrationScheduleRequestModel, RegistrationScheduleAllResponseModel>().ReverseMap();
            CreateMap<CreateRegistrationScheduleRequestModel, RegistraionSchedule>().ReverseMap();
            CreateMap<UpdateRegistrationScheduleRequestModel, RegistraionSchedule>().ReverseMap();


            //Accessory
            CreateMap<CreateAccessoryRequestModel, Accessory>().ReverseMap();
            CreateMap<UpdateAccessoryRequestModel, Accessory>().ReverseMap();
            CreateMap<AccessoryResponseModel, Accessory>().ReverseMap();

            //AccessoryKitTemplate
            CreateMap<CreateAccessoryKitTemplateRequestModel, AccessoryKitTemplate>().ReverseMap();
            CreateMap<UpdateAccessoryKitTemplateRequestModel, AccessoryKitTemplate>().ReverseMap();
            CreateMap<AccessoryKitTemplate, AccessoryKitTemplateResponseModel>()
                .ForMember(dest => dest.KitTemplateName, opt => opt.MapFrom(src => src.KitTemplate.KitTemplateName))
                .ForMember(dest => dest.AccessoryName, opt => opt.MapFrom(src => src.Accessory.AccessoryName))
                .ForMember(dest => dest.AccessoryValueCode, opt => opt.MapFrom(src => src.Accessory.AccessoryValueCode))
                .ReverseMap();

            //Grade
            CreateMap<CreateGradeRequestModel, Grade>().ReverseMap();
            CreateMap<UpdateGradeRequestModel, Grade>().ReverseMap();
            CreateMap<Grade, GradeResponseModel>()
            .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src => src.Teacher.UserFullName))
            .ForMember(dest => dest.TeacherId, opt => opt.MapFrom(src => src.Teacher.UserId))
            .ForMember(dest => dest.RegistraionScheduleId, opt => opt.MapFrom(src => src.RegistraionSchedule.RegistraionScheduleId))
            .ForMember(dest => dest.RegistraionScheduleName, opt => opt.MapFrom(src => src.RegistraionSchedule.RegistraionScheduleName))
            .ForMember(dest => dest.RegistraionScheduleDate, opt => opt.MapFrom(src => src.RegistraionSchedule.RegistraionScheduleDate))
            .ForMember(dest => dest.SlotName, opt => opt.MapFrom(src => src.RegistraionSchedule.Slot.SlotName))
            .ForMember(dest => dest.TimeStart, opt => opt.MapFrom(src => src.RegistraionSchedule.Slot.SlotStartTime))
            .ForMember(dest => dest.TimeEnd, opt => opt.MapFrom(src => src.RegistraionSchedule.Slot.SlotEndTime))
            .ForMember(dest => dest.TeamId, opt => opt.MapFrom(src => src.Team.TeamId))
            .ForMember(dest => dest.TeamName, opt => opt.MapFrom(src => src.Team.TeamName))
            .ReverseMap();

            //GradeDescription
            CreateMap<UpdateGradeDescriptionRequestModel, GradeDescription>().ReverseMap();
            CreateMap<GradeDescription, GradeDesciprionResponseModel>()
            .ForMember(dest => dest.ClassId, opt => opt.MapFrom(src => src.ClassId))
            .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.Class.ClassName))
            .ForMember(dest => dest.GradeId, opt => opt.MapFrom(src => src.GradeId))
            .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.StudentId))
            .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.UserFullName))
            .ForMember(dest => dest.RegistrationScheduleName, opt => opt.MapFrom(src => src.Grade.RegistraionSchedule.RegistraionScheduleName))
            .ForMember(dest => dest.RegistrationScheduleId, opt => opt.MapFrom(src => src.Grade.RegistraionScheduleId))
            .ForMember(dest => dest.SlotId, opt => opt.MapFrom(src => src.Grade.RegistraionSchedule.Slot.SlotId))
            .ForMember(dest => dest.SlotName, opt => opt.MapFrom(src => src.Grade.RegistraionSchedule.Slot.SlotName))
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.Grade.RegistraionSchedule.Slot.SlotStartTime))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.Grade.RegistraionSchedule.Slot.SlotEndTime))
            .ForMember(dest => dest.LabId, opt => opt.MapFrom(src => src.LabId))
            .ForMember(dest => dest.LabName, opt => opt.MapFrom(src => src.Lab.LabName))
            .ReverseMap();

            //KitAccessory
            CreateMap<CreateKitAccessoryRequestModel, KitAccessory>().ReverseMap();
            CreateMap<UpdateKitAccessoryRequestModel, KitAccessory>().ReverseMap();
            CreateMap<KitAccessory, KitAccessoryResponseModel>()
                .ForMember(dest => dest.KitName, opt => opt.MapFrom(src => src.Kit.KitName))
                .ForMember(dest => dest.AccessoryName, opt => opt.MapFrom(src => src.Accessory.AccessoryName))
                .ForMember(dest => dest.AccessoryValueCode, opt => opt.MapFrom(src => src.Accessory.AccessoryValueCode))
                .ReverseMap();


        }
    }
}
