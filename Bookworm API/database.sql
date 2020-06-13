use master
go
if(exists(select name from sys.databases where name = 'TCCF'))
	drop database TCCF
go
create database TCCF
go
use TCCF

create table tblProduto(
	IDProduto int identity primary key,
	TipoAcervo int not null, 
	NomeLivro varchar(70) not null,
	AutoresLivro varchar(100),
	AnoEdicao date,
	Setor int not null,
	Fileira int not null,
	Prateleira int not null,
	TipoProduto varchar(50),
	Editora varchar(50),
	DescricaoProd varchar(max),
	ImagemProd varbinary(max),
)

create table tblFuncionario(
	IDFuncionario int identity primary key,
	Nome varchar(50) not null,
	CPF char(11) not null, 
	Endereco varchar (150) not null,
	Telefone varchar(11) not null,
	Cargo varchar(50),
	Email varchar(50) not null,
	NivelAcesso int not null,
	Senha varchar(32) not null,
	Salt varchar(32) not null,
	ImagemFunc varbinary(max),
)

create table tblEvento(
	IDEvento int identity primary key,
	Titulo varchar(50) not null,
	Descricao varchar(max),
	Responsavel varchar (50),
	Email varchar(50) not null,
)

create table tblLeitor(
	IDLeitor int identity primary key,
	Nome varchar(50) not null,
	RG char(9) not null, 
	DataNasc date,
	Endereco varchar(150),
	Telefone varchar(11),
	TipoLeitor varchar(50) not null,
	Email varchar(50) not null,
	DataCadastro date not null,
	Senha varchar(32) not null,
	Salt varchar(32) not null,
	ImagemLeitor varbinary(max),
)

create table tblEmprestimo(
	IDEmprestimo int identity primary key,
	IDFuncionario int foreign key references tblFuncionario not null,
	IDProduto int foreign key references tblProduto not null,
	IDLeitor int foreign key references tblLeitor not null,
	DataRetirada date not null,
	DataEntrega date not null, 
	Renovacao int not null
)

create table tblReserva(
	IDProduto int foreign key references tblProduto,
	IDLeitor int foreign key references tblLeitor,
	DataReserva date not null, 
)

select i as testee from (select 'snad' as i) as _