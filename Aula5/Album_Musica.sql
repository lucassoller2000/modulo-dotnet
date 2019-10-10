create table Album(
idAlbum int identity primary key not null,
nome varchar(50) not null,
);

create table Musica(
idMusica int identity primary key not null,
nome varchar(50) not null,
duracao decimal(4,2) not null,
idAlbum int not null,
constraint FK_Musica_Album Foreign Key (idAlbum)
References Album (idAlbum)
); 

create table Usuario(
idUsuario int identity primary key not null,
nome varchar(50) not null
);

create table Avaliacao(
idAvaliacao int identity primary key not null,
nota int not null,
idMusica int not null,
idUsuario int not null,
constraint FK_Avaliacao_Musica Foreign Key (idMusica)
References Musica (idMusica),
constraint FK_Avalicao_Usuario Foreign Key (idUsuario)
References Usuario (idUsuario)
);

