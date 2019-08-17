# SourceASPCoreK7
Source code ASP.NET Core 2.2 khóa ngày 27/06/2019

## Buổi 1 (27/6/2019): Giới thiệu
* Làm quen Visual Studio 2019
* Mô hình MVC
* Gợi nhớ ngôn ngữ C#

## Buổi 2 (02/7/2019): HTML + CSS
* Các thẻ HTML thường dùng
* CSS cơ bản

## Buổi 3 (04/7/2019): CSS3
* Lab CSS: Sử dụng box-model, position,.. --> áp dụng đưa vào View

## Buổi 4 (06/07/2019): jQuery

## Buổi 5 (11/07/2019): BootStrap 4

## Buổi 6 (13/07/2019): MVC Basic

## Buổi 7 (16/07/2019): MVC Basic (tt)
* Async method
* Upload file

## Buổi 8 (18/07/2019): MVC Basic (tt)
* Read/Write file text/JSON

## Buổi 9 (20/07/2019): Validation

## Buổi 10 (23/07/2019): jQuery Validation

## Buổi 11 (25/07/2019): Layout
* @RenderBody(), @RenderSection()
* PartialView
* ViewComponent
* Area

## Buổi 12 (27/07/2019): SQL Basic
* DDL(Data Definition Language):
  *  CREATE, ALTER, DROP, TRUNCATE, RENAME
* DML(Data Manipulation Language)
  *  INSERT, UPDATE, DELETE, SELECT
  
## Buổi 13 (01/08/2019): SQL Advanced
* View, Store procedure, function, trigger
* TCL(transaction Control Language)
  *  COMMIT, ROLLBACK, SET TRANSACTION

## Buổi 14 (03/08/2019): ADO.NET
* CRUD with ADO.NET
  *  Prepare data: Table, Store procedure
  *  Create Data Model 
  *  Create Data Access Layer
  *  Create Controller/Action with View to CRUD
  
## Buổi 15 (06/08/2019): Intro EF Core
* Intro EF Core
* EF Core - Core first
* EF Core - Database first

## Buổi 16 (08/08/2019): EF Core - Database First

## Buổi 17 (10/08/2019): EF Core - Database First
	//1. Cài Nuget AutoMapper dành .NET Core

	AutoMapper.Extensions.Microsoft.DependencyInjection

	//2. Mở hàm ConfigureServices(), thêm khai báo

	services.AddAutoMapper();

	//3. Định nghĩa bộ map

	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<User, UserDto>();
			CreateMap<UserDto, User>();
		//Map 2 chiều
			//CreateMap<User, UserDto>().ReverseMap();
		}
	}

	//4. Ở đâu xài thì khai báo service

	public class XYZ
	{
		private readonly IMapper _mapper;
		public XYZ(IMapper map)
		{
			_mapper = map;
		}

		//sử dụng
		public void ABC()
		{
			var des = _mapper.Map<TDes>(source);
			var hhv = _mapper.Map<HangHoaViewModel>(hangHoa);
		}
	}

## Buổi 18 (13/08/2019): EF Core - Database First

## Buổi 19 (15/08/2019): EF Core - Code First

## Buổi 20 (17/08/2019): Session