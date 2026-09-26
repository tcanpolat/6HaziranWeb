use EducationDB

create procedure GetStudentsByDepartment @Department nvarchar(50)
as
begin
	select * from Students where Department = @Department
end

exec GetStudentsByDepartment 'Yazılım'