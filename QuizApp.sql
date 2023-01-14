IF EXISTS 
   (
     SELECT name FROM master.dbo.sysdatabases 
     WHERE name = N'QuizApp'
    )
DROP DATABASE QuizApp

CREATE DATABASE QuizApp
GO

USE QuizApp
GO

CREATE TABLE Users
(
    UserID INT PRIMARY KEY IDENTITY,
    Email VARCHAR(255) NOT NULL,
    Password VARCHAR(255) NOT NULL,
    FirstName VARCHAR(255) NOT NULL,
    LastName VARCHAR(255) NOT NULL,
    DateOfBirth DATE NOT NULL,
    Role VARCHAR(255) NOT NULL
);

CREATE TABLE Quiz
(
    QuizID INT PRIMARY KEY IDENTITY,
    QuizName VARCHAR(255) NOT NULL,
    QuizType VARCHAR(255) NOT NULL,
    TimeLimit INT NOT NULL
);

CREATE TABLE Category (
    CategoryID INT PRIMARY KEY IDENTITY(1,1),
    CategoryName NVARCHAR(255) NOT NULL
);


CREATE TABLE Question
(
    QuestionID INT PRIMARY KEY IDENTITY,
	CategoryID INT NOT NULL,
    QuestionText VARCHAR(MAX) NOT NULL,
    QuestionType VARCHAR(255) NOT NULL,
);
GO;
alter table Question

add  CategoryID int not null
Go
alter table Question
ADD FOREIGN KEY (CategoryID) REFERENCES Category(CategoryID);

CREATE TABLE Answer
(
    AnswerID INT PRIMARY KEY IDENTITY,
    QuestionID INT NOT NULL,
    AnswerText VARCHAR(255) NOT NULL,
    IsCorrect BIT NOT NULL,
    FOREIGN KEY (QuestionID) REFERENCES Question(QuestionID)
);

CREATE TABLE Result
(
    ResultID INT PRIMARY KEY IDENTITY,
    UserID INT NOT NULL,
    QuizID INT NOT NULL,
    StartTime DATETIME NOT NULL,
    EndTime DATETIME NOT NULL,
    Score INT NOT NULL,
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (QuizID) REFERENCES Quiz(QuizID)
);

CREATE TABLE Feedback
(
    FeedbackID INT PRIMARY KEY IDENTITY,
    UserID INT NOT NULL,
    Rating INT NOT NULL,
    FeedbackText VARCHAR(MAX) NOT NULL,
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

CREATE TABLE QuizQuestion (
    QuizQuestionID INT PRIMARY KEY IDENTITY(1,1),
    QuizID INT FOREIGN KEY REFERENCES Quiz(QuizId),
    QuestionID INT FOREIGN KEY REFERENCES Question(QuestionId),
    AnswerID INT FOREIGN KEY REFERENCES Answer(AnswerId)
);


INSERT INTO Category ( CategoryName) VALUES ( 'Math');
INSERT INTO Category ( CategoryName) VALUES ( 'Science');
INSERT INTO Category ( CategoryName) VALUES ( 'History');

INSERT INTO Users (Email, Password, FirstName, LastName, DateOfBirth, Role) VALUES ('johndoe@email.com', 'password123', 'John', 'Doe', '1997-01-01', 'user');
INSERT INTO Users (Email, Password, FirstName, LastName, DateOfBirth, Role) VALUES ('janedoe@email.com', 'password456', 'Jane', 'Doe', '1995-02-14', 'admin');
INSERT INTO Users (Email, Password, FirstName, LastName, DateOfBirth, Role) VALUES ('robert@email.com', 'password789', 'Robert', 'Smith', '1989-03-21', 'user');
INSERT INTO Users (Email, Password, FirstName, LastName, DateOfBirth, Role) VALUES ('ashley@email.com', 'password000', 'Ashley', 'Johnson', '1992-04-05', 'admin');
INSERT INTO Users (Email, Password, FirstName, LastName, DateOfBirth, Role) VALUES ('michael@email.com', 'password111', 'Michael', 'Williams', '1988-05-12', 'user');

INSERT INTO Quiz (QuizName, QuizType, TimeLimit) VALUES
('Math Quiz', 'Multi-choice', 15),
('Science Quiz', 'Multi-choice', 15),
('History Quiz', 'Multi-choice', 15);

--QuestionID from 84-103 is Math category

INSERT INTO Question (  CategoryID, QuestionText, QuestionType)
VALUES (  1, 'What is the value of x in the equation 2x + 4 = 10?', 'Math')
INSERT INTO Question (  CategoryID, QuestionText, QuestionType)
VALUES (  1, 'What is the area of a circle with a radius of 5?', 'Math')
INSERT INTO Question (  CategoryID, QuestionText, QuestionType)
VALUES (  1, 'Simplify the expression 3x^2 + 4x - 2x^2', 'Math')
INSERT INTO Question (  CategoryID, QuestionText, QuestionType)
VALUES (  1, 'What is the slope of the line y = 2x + 1?', 'Math')

INSERT INTO Question ( CategoryID, QuestionText, QuestionType)
VALUES (  1, 'What is the equation of the line that passes through the points (2, 3) and (4, 7)?', 'Math')
INSERT INTO Question (  CategoryID, QuestionText, QuestionType)
VALUES (  1, 'What is the value of sin(60) in radians?', 'Math')
INSERT INTO Question (  CategoryID, QuestionText, QuestionType)
VALUES ( 1, 'What is the value of e^3?', 'Math')
INSERT INTO Question (  CategoryID, QuestionText, QuestionType)
VALUES (  1, 'Solve the system of equations: x + 2y = 4 and 3x - 4y = 2', 'Math')
--
INSERT INTO Question (  CategoryID, QuestionText, QuestionType)
VALUES (  1, 'Simplify the expression (3x + 4)(2x - 5)', 'Math')
INSERT INTO Question (  CategoryID, QuestionText, QuestionType)
VALUES (  1, 'What is the value of the determinant of the matrix |1 2| |3 4|', 'Math')
INSERT INTO Question (  CategoryID, QuestionText, QuestionType)
VALUES (  1, 'What is the inverse of the matrix |2 3| |1 4|', 'Math')
INSERT INTO Question (  CategoryID, QuestionText, QuestionType)
VALUES (  1, 'What is the equation of the parabola with vertex (3, 2) and directrix y = -1?', 'Math')

INSERT INTO Question ( CategoryID, QuestionText, QuestionType)
VALUES (  1, 'What is the equation of the ellipse with center (2, 3) and major and minor axes of length 6 and 4 respectively?', 'Math')
INSERT INTO Question (  CategoryID, QuestionText, QuestionType)
VALUES (  1, 'What is the equation of the hyperbola with center (5, 6) and asymptotes y = ±(1/2)x + 4?', 'Math')
INSERT INTO Question ( CategoryID, QuestionText, QuestionType)
VALUES (  1, 'What is the equation of the circle with center (3, 2) and radius 5?', 'Math')
INSERT INTO Question (  CategoryID, QuestionText, QuestionType)
VALUES ( 1, 'What is the equation of the line in standard form that passes through the points (1, 2) and (3, 4)?', 'Math')

INSERT INTO Question (  CategoryID, QuestionText, QuestionType)
VALUES (  1, 'What is the value of the integral ∫(x^2 + 1) dx from 0 to 2?', 'Math')
INSERT INTO Question (  CategoryID, QuestionText, QuestionType)
VALUES (  1, 'What is the value of the limit (x^2 - 4x + 4)/(x - 2) as x approaches 2?', 'Math')
INSERT INTO Question (  CategoryID, QuestionText, QuestionType)
VALUES (  1, 'What is the equation of the line in slope-intercept form that is parallel to the line y = 2x + 1 and passes through the point (3, 4)?', 'Math')
INSERT INTO Question (  CategoryID, QuestionText, QuestionType)
VALUES (  1, 'What is the equation of the line in point-slope form that has a slope of -3 and passes through the point (4, 2)?', 'Math')

--answers for math
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (84, 'x = 2', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (84, 'x = 4', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (84, 'x = 6', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (84, 'x = 8', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (85, '78.5', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (85, '25', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (85, '50', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (85, '100', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (86, 'x^2 + x - 2', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (86, 'x^2 - 2x + 4', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (86, '2x^2 + 4x - 8', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (86, '3x^2 - 2x + 4', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (87, '2', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (87, '-2', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (87, '4', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (87, '-4', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (88, 'y = 2x + 5', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (88, 'y = 2x - 1', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (88, 'y = 4x + 2', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (88, 'y = 2x - 5', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (89, '0.5236', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (89, '1.0472', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (89, '2.0944', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (89, '3.0944', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (90, '20.0855', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (90, '4', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (90, '6', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (90, '8', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (91, 'x = 1, y = 2', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (91, 'X = 3, y = 5 ', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (91, 'X = 2, y = 1', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (91, 'X = 5, y = 5', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (92, '6x^2 - 17x - 5', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (92, '10x^2 - 19x - 8', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (92, '8x^2 - 27x - 4', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (92, '9x^2 - 13x - 6', 0);

--
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (93, '5', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (93, '8', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (93, '3', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (93, '7', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (94, '-0.5  1.5', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (94, '-0.76  1.9', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (94, '-0.1  2.5', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (94, '-0.9  1.5', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (95, '(x - 3)^2 = 4(y - 2)', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (95, '(x - 9)^2 = 5(y - 8)', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (95, '(x - 5)^2 = 2(y - 3)', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (95, '(x - 7)^2 = 6(y - 9)', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (96, '(x - 2)^2/4 + (y - 3)^2/9 = 1', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (96, '(x - 9)^3/4 + (y - 8)^5/8 = 3', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (96, '(x - 5)^2/8 + (y - 6)^2/7 = 5', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (96, '(x - 3)^7/9 + (y - 7)^4/9 = 6', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (97, 'y = (1/2)x + 8 or y = -(1/2)x + 2', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (97, 'y = (1/4)x + 7 or y = -(1/4)x + 6', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (97, 'y = (1/6)x + 9 or y = -(1/6)x + 8', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (97, 'y = (1/7)x + 6 or y = -(1/7)x + 9', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (98, '(x - 3)^2 + (y - 2)^2 = 25', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (98, '(x - 9)^7 + (y - 5)^3 = 36', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (98, '(x - 6)^9 + (y + 9)^2 = 27', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (98, '(x + 8)^2 + (y - 9)^2 = 35', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (99, 'y - 2 = -(3/4)(x - 1)', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (99, 'y + 6 = -(6/9)(x - 8)', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (99, 'y - 8 = -(3/4)(x + 9)', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (99, 'y + 2 = -(8/9)(x + 6)', 0);


INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (100, 'y - 2 = -3(x - 4)', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (100, 'y + 3 = -7(x + 9)', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (100, 'y + 6 = -8(x - 6)', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (100, 'y - 7 = -5(x - 7)', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (101, '3', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (101, '5', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (101, '7', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (101, '9', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (102, '10/3', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (102, '8/3', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (102, '20/9', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (102, '15/7', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (103, 'y = 2x + 1', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (103, 'y = 9x - 3', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (103, 'y = 5x + 8', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (103, 'y = 7x - 9', 0);

--questions for science
INSERT INTO Question (CategoryID, QuestionText, QuestionType) VALUES
(2, 'What is the chemical symbol for Oxygen?', 'Science');
INSERT INTO Question (CategoryID, QuestionText, QuestionType) VALUES
(2, 'What is the chemical formula for water?', 'Science');
INSERT INTO Question (  CategoryID, QuestionText, QuestionType)
VALUES (2, 'What is the process called where plants use energy from the sun to convert carbon dioxide and water into glucose and oxygen?', 'Science')
INSERT INTO Question (  CategoryID, QuestionText, QuestionType)
VALUES (2, 'What is the name of the largest bone in the human body?', 'Science')

INSERT INTO Question ( CategoryID, QuestionText, QuestionType)
VALUES (2, 'What is the process called where a solid turns directly into a gas?', 'Science')
INSERT INTO Question (  CategoryID, QuestionText, QuestionType)
VALUES (2, 'What is the name of the outermost layer of the Earth?', 'Science')
INSERT INTO Question (  CategoryID, QuestionText, QuestionType)
VALUES (2, 'What is the name of the process where living organisms change the environment they live in?', 'Science')
INSERT INTO Question (  CategoryID, QuestionText, QuestionType)
VALUES (2, 'What is the name of the process where rock is worn away by wind and water?', 'Science')
--
INSERT INTO Question (  CategoryID, QuestionText, QuestionType)
VALUES (2, 'What is the name of the process where rock is broken down into smaller pieces?', 'Science')
INSERT INTO Question (  CategoryID, QuestionText, QuestionType)
VALUES (2, 'What is the name of the process where plants and animals turn dead organic matter into soil?', 'Science')
INSERT INTO Question (  CategoryID, QuestionText, QuestionType)
VALUES (2, 'What is the name of the process where living organisms change the composition of the air?', 'Science')
INSERT INTO Question (  CategoryID, QuestionText, QuestionType)
VALUES (2, 'What is the name of the process where living organisms change the composition of the water?', 'Science')

INSERT INTO Question ( CategoryID, QuestionText, QuestionType)
VALUES (2, 'What is the name of the process where living organisms change the temperature of the environment?', 'Science')
INSERT INTO Question (  CategoryID, QuestionText, QuestionType)
VALUES (2, 'What is the name of the process where living organisms change the amount of light in the environment?', 'Science')
INSERT INTO Question ( CategoryID, QuestionText, QuestionType)
VALUES (2, 'What is the name of the process where living organisms change the amount of sound in the environment?', 'Science')
INSERT INTO Question (  CategoryID, QuestionText, QuestionType)
VALUES (2, 'What is the name of the process where living organisms change the amount of movement in the environment?', 'Science')

INSERT INTO Question (  CategoryID, QuestionText, QuestionType)
VALUES (2, 'What is the name of the process where living organisms change the amount of space in the environment?', 'Science')
INSERT INTO Question (  CategoryID, QuestionText, QuestionType)
VALUES (2, 'What is the name of the process where living organisms change the amount of nutrients in the environment?', 'Science')
INSERT INTO Question (  CategoryID, QuestionText, QuestionType)
VALUES (2, 'What is the name of the process where living organisms change the amount of waste in the environment?', 'Science')
INSERT INTO Question (CategoryID, QuestionText, QuestionType) VALUES (2, 'What is the chemical formula for water?', 'Science')

--science answers
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES
(104, 'The study of life and living organisms', 0)
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (104, 'The study of the physical and natural world through observation and experimentation', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (104, 'The study of human behavior and society', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (104, 'The study of the natural and supernatural world', 0)

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (105, 'The study of the universe and its origins', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (105, 'The study of the human mind and behavior', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (105, 'The study of living organisms and their interactions with each other and their environment', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (105, 'The study of the earth and its processes', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (106, 'A measure of the amount of matter in an object', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (106, 'A measure of how heavy an object is', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (106, 'A measure of how hot or cold an object is', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (106, 'A measure of how dense an object is', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (107, 'The study of the structure, properties, and behavior of matter', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (107, 'The study of the universe and its origins', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (107, 'The study of living organisms and their interactions with each other and their environment', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (107, 'The study of the earth and its processes', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (108, 'A type of matter that has a definite shape and volume', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (108, 'A type of matter that has no definite shape and takes the shape of its container', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (108, 'A type of matter that has no definite volume and takes the volume of its container', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (108, 'A type of matter that has a definite shape but no definite volume', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (109, 'The study of the behavior, properties, and interactions of energy', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (109, 'The study of the universe and its origins', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (109, 'The study of living organisms and their interactions with each other and their environment', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (109, 'The study of the earth and its processes', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (110, 'The ability to do work', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (110, 'The ability to move', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (110, 'The ability to heat up', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (110, 'The ability to produce light', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (111, 'The study of the behavior and properties of sound', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (111, 'The study of the behavior and properties of light', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (111, 'The study of the behavior and properties of electricity', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (111, 'The study of the behavior and properties of magnetism', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (112, 'The study of the behavior and properties of electricity', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (112, 'The study of the behavior and properties of light', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (112, 'The study of the behavior and properties of air', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (112, 'The study of the behavior and properties of fire', 0);

--
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (113, 'The study of the physical and natural phenomena of the universe is known as astronomy', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (113, 'The study of the physical and natural phenomena of the universe is known as history', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (113, 'The study of the physical and natural phenomena of the universe is known as music', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (113, 'The study of the physical and natural phenomena of the universe is known as English', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (114, 'The study of the properties, structure, and behavior of matter and energy is known as physics', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (114, 'The study of the properties, structure, and behavior of matter and energy is known as chemistry', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (114, 'The study of the properties, structure, and behavior of matter and energy is known as philosophy', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (114, 'The study of the properties, structure, and behavior of matter and energy is known as math', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (115, 'The study of the Earth and its history is known as geology', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (115, 'The study of the Earth and its history is known as astonomy', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (115, 'The study of the Earth and its history is known as geography', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (115, 'The study of the Earth and its history is known as physics', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (116, 'The study of living organisms and their interactions with each other and the environment is known as biology', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (116, 'The study of living organisms and their interactions with each other and the environment is known as geography', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (116, 'The study of living organisms and their interactions with each other and the environment is known as chemistry', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (116, 'The study of living organisms and their interactions with each other and the environment is known as geology', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (117, 'The study of the universe and its origins is known as cosmology', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (117, 'The study of the universe and its origins is known as bibliography', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (117, 'The study of the universe and its origins is known as chemistry', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (117, 'The study of the universe and its origins is known as geology', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (118, 'The study of the oceans and their inhabitants is known as oceanography', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (118, 'The study of the oceans and their inhabitants is known as cosmology', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (118, 'The study of the oceans and their inhabitants is known as geography', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (118, 'The study of the oceans and their inhabitants is known as philosophy', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (119, 'The study of the atmosphere and weather is known as meteorology', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (119, 'The study of the atmosphere and weather is known as geography', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (119, 'The study of the atmosphere and weather is known as chemistry', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (119, 'The study of the atmosphere and weather is known as cosmology', 0);


INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (120, 'The study of the properties and behavior of matter and energy at a microscopic level is known as quantum mechanics', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (120, 'The study of the properties and behavior of matter and energy at a microscopic level is known as chemical mechanics', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (120, 'The study of the properties and behavior of matter and energy at a microscopic level is known as partical mechanics', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (120, 'The study of the properties and behavior of matter and energy at a microscopic level is known as ocean mechanics', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (121, 'The study of the properties and behavior of matter and energy at a macroscopic level is known as classical mechanics', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (121, 'The study of the properties and behavior of matter and energy at a macroscopic level is known as geologic mechanics', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (121, 'The study of the properties and behavior of matter and energy at a macroscopic level is known as chemical mechanics', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (121, 'The study of the properties and behavior of matter and energy at a macroscopic level is known as quantum mechanics', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (122, 'The study of the properties and behavior of light is known as optics', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (122, 'The study of the properties and behavior of light is known as lightnning', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (122, 'The study of the properties and behavior of light is known as oceangraphy', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (122, 'The study of the properties and behavior of light is known as cosmology', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (123, 'The study of the properties and behavior of sound is known as acoustics', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (123, 'The study of the properties and behavior of sound is known as music', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (123, 'The study of the properties and behavior of sound is known as rythm', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (123, 'The study of the properties and behavior of sound is known as melody', 0);

--questions for history
INSERT INTO Question (CategoryID, QuestionText, QuestionType) VALUES
 (3, 'What was the main cause of World War I?', 'History');
INSERT INTO Question (CategoryID, QuestionText, QuestionType) VALUES
(3, 'Who were the main leaders of the American Revolution?', 'History');
INSERT INTO Question (  CategoryID, QuestionText, QuestionType)
VALUES (3, 'What was the main goal of the civil rights movement in the 1960s?', 'History')
INSERT INTO Question (  CategoryID, QuestionText, QuestionType)
VALUES (3, 'What event sparked the start of World War II?', 'History')

INSERT INTO Question ( CategoryID, QuestionText, QuestionType)
VALUES (3, 'Who were the main leaders of the French Revolution?', 'History')
INSERT INTO Question (  CategoryID, QuestionText, QuestionType)
VALUES (3, 'What was the main outcome of the Battle of Waterloo?', 'History')
INSERT INTO Question (  CategoryID, QuestionText, QuestionType)
VALUES (3, 'What was the main cause of the fall of the Western Roman Empire?', 'History')
INSERT INTO Question (  CategoryID, QuestionText, QuestionType)
VALUES (3, 'Who was the main leader of the Russian Revolution?', 'History')
--
INSERT INTO Question (  CategoryID, QuestionText, QuestionType)
VALUES (3, 'What was the main cause of the Industrial Revolution?', 'History')
INSERT INTO Question (  CategoryID, QuestionText, QuestionType)
VALUES (3, 'What was the main outcome of the Hundred Years War?', 'History')
INSERT INTO Question (  CategoryID, QuestionText, QuestionType)
VALUES (3, 'What was the main cause of the American Civil War?', 'History')
INSERT INTO Question (  CategoryID, QuestionText, QuestionType)
VALUES (3, 'Who were the main leaders of the Scientific Revolution?', 'History')

INSERT INTO Question ( CategoryID, QuestionText, QuestionType)
VALUES (3, 'What was the main cause of the Peloponnesian War?', 'History')
INSERT INTO Question (  CategoryID, QuestionText, QuestionType)
VALUES (3, 'What was the main outcome of the Punic Wars?', 'History')
INSERT INTO Question ( CategoryID, QuestionText, QuestionType)
VALUES (3, 'What was the main cause of the Seven Years War?', 'History')
INSERT INTO Question (  CategoryID, QuestionText, QuestionType)
VALUES (3, 'Who were the main leaders of the Enlightenment?', 'History')

INSERT INTO Question (  CategoryID, QuestionText, QuestionType)
VALUES (3, 'What was the main outcome of the War of 1812?', 'History')
INSERT INTO Question (  CategoryID, QuestionText, QuestionType)
VALUES (3, 'What was the main cause of the English Civil War?', 'History')
INSERT INTO Question (  CategoryID, QuestionText, QuestionType)
VALUES (3, 'What was the main outcome of the Napoleonic Wars?', 'History')
INSERT INTO Question (CategoryID, QuestionText, QuestionType) VALUES (3, 'What event marked the end of World War II?', 'History');

--history answers
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES
(124, '1492', 1)
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (124, '1495', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (124, '1594', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (124, '1489', 0)

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (125, 'The Magna Carta', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (125, 'The Hitlar', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (125, 'The Nazi', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (125, 'The Scout', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (126, 'The French Revolution', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (126, 'The Brazil Revolution', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (126, 'The South Africa Revolution', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (126, 'The British Revolution', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (127, 'The Berlin Wall', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (127, 'The French Wall', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (127, 'The British Wall', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (127, 'The America Wall', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (128, 'The Roman Empire', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (128, 'The British Empire', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (128, 'The Whels Empire', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (128, 'The French Empire', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (129, 'The Industrial Revolution', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (129, 'The Economic Revolution', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (129, 'The Planting Revolution', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (129, 'The Railroad Revolution', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (130, 'The American Revolution', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (130, 'The French Revolution', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (130, 'The Brazil Revolution', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (130, 'The British Revolution', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (131, 'The Civil War', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (131, 'The Cold War', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (131, 'The Pandemic War', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (131, 'The Inner War', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (132, 'The Cold War', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (132, 'The Civil War', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (132, 'The Outer War', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (132, 'The Pandemic War', 0);

--
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (133, 'The Holocaust', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (133, 'The Persecution', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (133, 'The Catastrophe', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (133, 'The Shoah', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (134, 'World War I', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (134, 'World War II', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (134, 'World War III', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (134, 'World War IV', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (135, 'World War II', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (135, 'World War I', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (135, 'World War III', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (135, 'TWorld War IV', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (136, 'The Cuban Missile Crisis', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (136, 'The Indunisia Missile Crisis', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (136, 'The Columbia Missile Crisis', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (136, 'The India Missile Crisis', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (137, 'The Vietnam War', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (137, 'The China War', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (137, 'The French War', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (137, 'The Malaysia War', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (138, 'The Korean War', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (138, 'The Vietnam War', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (138, 'The Japan War', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (138, 'The Maylaysia War', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (139, 'The Gulf War', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (139, 'The Vietnam War', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (139, 'The Korea War', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (139, 'The Japan War', 0);


INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (140, 'The Iraq War', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (140, 'The Vietnam War', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (140, 'The Gulf War', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (140, 'The Korea War', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (141, 'The War in Afghanistan', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (141, 'The War in Korea', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (141, 'The War in China', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (141, 'The War in Japan', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (142, 'The Syrian Civil War', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (142, 'The Korea Civil War', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (142, 'The Japan Civil War', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (142, 'The Columbia Civil War', 0);

INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (143, 'The Russian Revolution', 1);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (143, 'The French Revolution', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (143, 'The British Revolution', 0);
INSERT INTO Answer (QuestionID, AnswerText, IsCorrect) VALUES (143, 'The American Revolution', 0);


--delete from Question
--drop table Question

