IF NOT EXISTS (SELECT 1 FROM dbo.Users WHERE Id = '6b590f74-d14e-45bb-936c-3e445212be68')
BEGIN
	INSERT INTO dbo.Users(Id, Username, IsActive, PasswordHash, Email)
	VALUES('6b590f74-d14e-45bb-936c-3e445212be68', 'Viktor', 0, '$2a$11$s4MfHDRhjkftem7itOJC5OKDUoqBWfDl7sg5Z2qn5InkONUkOcOxK', 'viktor1@mail.ru');
END

IF NOT EXISTS (SELECT 1 FROM dbo.Users WHERE Id = 'ed803197-bcd6-41a4-810c-fa1637093504')
BEGIN
	INSERT INTO dbo.Users(Id, Username, IsActive, PasswordHash, Email)
	VALUES('ed803197-bcd6-41a4-810c-fa1637093504', 'Maks', 0, '$2a$11$s4MfHDRhjkftem7itOJC5OKDUoqBWfDl7sg5Z2qn5InkONUkOcOxK', 'maks@mail.ru');
END

IF NOT EXISTS (SELECT 1 FROM dbo.Users WHERE Id = 'ba560fb5-8156-4997-9acd-a6bc7964e554')
BEGIN
	INSERT INTO dbo.Users(Id, Username, IsActive, PasswordHash, Email)
	VALUES('ba560fb5-8156-4997-9acd-a6bc7964e554', 'Stepan', 1, '$2a$11$s4MfHDRhjkftem7itOJC5OKDUoqBWfDl7sg5Z2qn5InkONUkOcOxK', 'stepan@mail.ru');
END

IF NOT EXISTS (SELECT 1 FROM dbo.Users WHERE Id = 'b8516da2-6421-4539-a0e3-9e608a8860bd')
BEGIN
	INSERT INTO dbo.Users(Id, Username, IsActive, PasswordHash, Email)
	VALUES('b8516da2-6421-4539-a0e3-9e608a8860bd', 'Anna', 0, '$2a$11$s4MfHDRhjkftem7itOJC5OKDUoqBWfDl7sg5Z2qn5InkONUkOcOxK', 'anna@mail.ru');
END

IF NOT EXISTS (SELECT 1 FROM dbo.Users WHERE Id = 'ed91cd00-812e-4b16-af47-d929a6ab2b0b')
BEGIN
	INSERT INTO dbo.Users(Id, Username, IsActive, PasswordHash, Email)
	VALUES('ed91cd00-812e-4b16-af47-d929a6ab2b0b', 'Sergey', 1, '$2a$11$s4MfHDRhjkftem7itOJC5OKDUoqBWfDl7sg5Z2qn5InkONUkOcOxK', 'sergey@mail.ru');
END

IF NOT EXISTS (SELECT 1 FROM dbo.Users WHERE Id = '0eaaafa9-b19a-4cb5-96c9-3dc637536e3f')
BEGIN
	INSERT INTO dbo.Users(Id, Username, IsActive, PasswordHash, Email)
	VALUES('0eaaafa9-b19a-4cb5-96c9-3dc637536e3f', 'Tolya', 1, '$2a$11$s4MfHDRhjkftem7itOJC5OKDUoqBWfDl7sg5Z2qn5InkONUkOcOxK', 'tolya@mail.ru');
END