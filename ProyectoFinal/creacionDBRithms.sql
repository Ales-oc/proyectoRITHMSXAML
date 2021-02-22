CREATE DATABASE Rithms
USE Rithms

CREATE TABLE Usuarios(
	id_user INT IDENTITY(1,1) PRIMARY KEY,
	nickname VARCHAR(50) UNIQUE,
	correo VARCHAR(80) UNIQUE,
	password VARBINARY(MAX),
	fecha_nacimiento DATETIME,
	Admin BIT
);

INSERT INTO Usuarios VALUES
('admin_aoc', 'alejandroortizcaraballo@gmail.com', ENCRYPTBYPASSPHRASE('password', 'admin_aoc'), '14/02/2021', 1);

CREATE TABLE Canciones(
	id_song INT IDENTITY(1,1) PRIMARY KEY,
	autor VARCHAR(60),
	titulo NVARCHAR(80),
	genero NVARCHAR(50),
	archivo VARBINARY(MAX)
);

CREATE TABLE Playlists(
	id_playlist INT IDENTITY(1,1) PRIMARY KEY,
	titulo VARCHAR(80),
	id_user INT UNIQUE,
	CONSTRAINT FK_idUsuario FOREIGN KEY (id_user)
	REFERENCES Usuarios(id_user) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE Canc_Playl(
	id_song INT,
	id_playlist INT,
	fecha_alta DATETIME,
	PRIMARY KEY(id_song, id_playlist),
	CONSTRAINT FK_idSong FOREIGN KEY (id_song)
	REFERENCES Canciones(id_song) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT FK_idPlaylist FOREIGN KEY (id_playlist)
	REFERENCES Playlists(id_playlist) ON DELETE CASCADE ON UPDATE CASCADE
);

SELECT CONVERT(VARCHAR(MAX), DECRYPTBYPASSPHRASE('password', password)) FROM Usuarios WHERE nickname='admin_aoc'
