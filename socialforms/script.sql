CREATE DATABASE IF NOT EXISTS socialforms;
USE socialforms;

CREATE TABLE users(
    userId INT NOT NULL AUTO_INCREMENT,
    userName VARCHAR(255) NOT NULL,
    email VARCHAR(255) NOT NULL,
    pass VARCHAR(385) NOT NULL, --hier muss ein hash rein dies das, kein Klartext
    birthDate DATE,
    userDescription VARCHAR(1024),
    picture MEDIUMBLOB,

    PRIMARY KEY (userId))
);

CREATE TABLE forms(
    formId INT NOT NULL AUTO_INCREMENT,
    userId INT NOT NULL,
    formName VARCHAR(255) NOT NULL,
    createDate DATE NOT NULL,

    PRIMARY KEY(formId),
    FOREIGN KEY(userId) REFERENCES users(userId)
);

CREATE TABLE questions(
    questionId INT NOT NULL AUTO_INCREMENT,
    formId INT NOT NULL,
    text VARCHAR(1024) NOT NULL,
    questionType INT NOT NULL,

    PRIMARY KEY(questionId),
    FOREIGN KEY(formId) REFERENCES forms(formId)
);

CREATE TABLE answers(
    answerId INT NOT NULL AUTO_INCREMENT,
    questionId INT NOT NULL,
    userId INT NOT NULL,
    textAnswer VARCHAR(1024),
    choiceAnswer INT,
    
    

    PRIMARY KEY(answerId),
    FOREIGN KEY(questionId) REFERENCES questions(questionId),
    FOREIGN KEY(userId) REFERENCES answers(answerId)
);