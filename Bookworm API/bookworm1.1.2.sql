use master
go
if(exists(select name from sys.databases where name = 'TCCF'))
	drop database TCCF
go
create database TCCF
go
use TCCF

Create table tblProduto(
	IDProduto int identity primary key,
	TipoAcervo int not null, 
	NomeLivro varchar(70) not null,
	AutoresLivro varchar(100),
	AnoEdicao int,
	Setor int not null,
	Fileira int not null,
	Prateleira int not null,
	TipoProduto varchar(50),
	Editora varchar(50),	
)

Create table tblFuncionario(
	IDFuncionario int identity primary key,
	Nome varchar(50) not null,
	CPF char(11) not null, 
	Endereco varchar (100) not null,
	Telefone varchar(11) not null,
	Cargo varchar(50),
	Email varchar(50),
	NivelAcesso int not null,
	Senha  varchar(15),
)

Create table tblEvento(
	IDEvento int identity primary key,
	Titulo varchar(50) not null,
	Descricao varchar(max),
	Responsavel varchar (50),
	Email varchar(50),
)

Create table tblLeitor(
	IDLeitor int identity primary key,
	Nome varchar(50) not null,
	RG char(9) not null, 
	DataNasc date,
	Endereco varchar(100),
	Telefone varchar(11),
	TipoLeitor int not null,
	Email varchar(50),
	Senha varchar(15) not null,
	DataCadastro date not null,
)

Create table tblEmprestimo(
	IDEmprestimo int identity primary key,
	IDFuncionario int foreign key references tblFuncionario null,
	IDProduto int foreign key references tblProduto not null,
	IDLeitor int foreign key references tblLeitor not null,
	DataRetirada date not null,
	DataEntrega date not null, 
	Renovacao int not null
)

Create table tblReserva(
	IDProduto int foreign key references tblProduto,
	IDLeitor int foreign key references tblLeitor,
	DataReserva date not null, 
)

insert tblEvento values ('Palestra 1', 'Palestra de testes para testar', 'Sávio Alves', 'savioacp@gmail.com'),
						('Coronavírus', 'Sobre o coronavirus, o que é? de onde veio? como é? de onde é? quando é?', 'Saulo Aulo', 'savioacpacp@gmail.com')

insert tblFuncionario values	('Sávio Alves', '47939319876', 'Rua Castanhal, 165 Casa 2 - C. Patriarca, São Paulo, SP', '11968518997', 'Administador', 'savioacp@gmail.com', 0, ''),
								('Juliana Fusco', '1234567890X', 'Rua Grande Grande pra Grande, 123 - Lá, Pá, São Paulo, SP', '11987654321', 'Administador', 'julic.fusco@gmail.com', 0, '')

select * from tblEvento where Titulo like '%Coro%'