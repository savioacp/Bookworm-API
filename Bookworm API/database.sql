use master
go
if(exists(select name from sys.databases where name = 'TCCF'))
	drop database TCCF
go
create database TCCF
go
use TCCF

create table tblGenero (
	IDGenero int identity primary key,
	NomeGenero varchar(30)
)

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

create table tblGeneroProduto (
	IDGenero int foreign key references tblGenero,
	IDProduto int foreign key references tblProduto
)

create table tblCargo (
	IDCargo int identity primary key,
	NomeCargo varchar(50),
	NivelAcesso int not null
)

create table tblFuncionario(
	IDFuncionario int identity primary key,
	IDCargo int foreign key references tblCargo,
	Nome varchar(50) not null,
	CPF char(11) not null,
	RG char(9) not null, 
	Endereco varchar (150) not null,
	Telefone varchar(11) not null,
	Email varchar(50) not null,
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

create table tblTipoLeitor (
	IDTipoLeitor int identity primary key,
	TipoLeitor varchar(50) not null
)

create table tblLeitor(
	IDLeitor int identity primary key,
	IDTipoLeitor int foreign key references tblTipoLeitor,
	Nome varchar(50) not null,
	CPF char(11) not null,
	RG char(9) not null, 
	DataNasc date,
	Endereco varchar(150),
	Telefone varchar(11),
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

insert tblGenero values ('Aventura'),	-- 1
						('Fantasia'),	-- 2
						('Romance'),	-- 3
						('Tristeza'),	-- 4
						('Terror'),		-- 5
						('Suspense'),	-- 6
						('Cat�lico'),	-- 7
						('Anima��o'),	-- 8
						('Arte'),		-- 9
						('Mang�'),		-- 10
						('Suspense'),	-- 11
						('Rock')		-- 12



insert tblProduto values (0, 'Senhor dos An�is: A Sociedade do Anel', 'J. R. R. Tolkien', '2003', 0, 0, 0, 'Livro', 'George Allen & Unwin', 'Este livro leva-nos para um mundo onde an�is forjados por an�es reinam com o seu poder. No entanto, h� um anel que � o mais poderoso de todos e, se cair em m�os erradas, pode ter um poder destrutivo. � o que, infelizmente acontece. Mas depois, este perde-se e passado algum tempo, vai parar �s m�os de um hobbit chamado Frodo Bolseiro e, este, ao n�o saber o que fazer com ele, decide consultar um amigo feiticeiro de nome Gandalf, o Cinzento. Fica decidido ir � bela cidade �lfica de Valfenda para fazer um conselho, ministrado pelo s�bio Elrond, que determinar� quem vai � terra sombria de Mordor e destruir l�, na Montanha da Perdi��o, o Anel, o �nico lugar em que tal artefato pode ser destru�do. S�o escolhido o hobbit e mais oito companheiros para realizar tal perigosa miss�o: os tamb�m hobbits Samwise "Sam" Gamgi (fiel companheiro de Frodo), Meriadoc "Merry" Brandebuque e Peregrin "Pippin" T�k (representando os hobbits), o mago Gandalf (representando a ordem m�gica dos Istari), os humanos Aragorn e Boromir (representando os homens), o elfo Legolas (representando os elfos) e o an�o Gimli (representando os an�es).', 0x00)
insert tblGeneroProduto values	(1,1),
								(2,1)


insert tblProduto values (0, 'Ela e o Seu Gato', 'Makoto Shinkai, Tsubasa Yamaguchi', '2016', 0, 0, 0, 'Livro', 'NewPOP', '"Era um dia de chuva, no come�o da primavera. Eu fui acolhido por ela;" Um gato e uma garota que mora sozinha se conhecem na primavera... Ao viver sozinha, ela aprende a alegria e a solid�o de ser independente, enquanto o gato, que recebeu o nome de Chobi, descobri a exist�ncia do mundo atrav�s dessa garota. O tempo desses dois passa lentamente, mas a severidade do mundo acaba por alcan��-la...', 0x00)
insert tblGeneroProduto values	(3,2),
								(4,2)


insert tblProduto values (0, 'The Legend of Zelda: Majoras Mask, A Link To The Past; Perfect Edition', 'Akira Himekawa', '2018', 0, 0, 0, 'Revista', 'Planet Manga', 'Em sua viagem de treinamento, Link tem sua ocarina roubada por uma estranha criatura mascarada e � transformado em um deku scrub! Agora, Link deve correr contra o tempo para salvar a Cidade Rel�gio da destrui��o iminente e recuperar a M�scara de Majora!
Na segunda hist�ria, Link acorda inquieto ao ouvir um chamado. A princesa Zelda foi raptada pelo maligno Agahnim, um seguidor de Ganon que deseja a Triforce para si. Link parte em uma jornada para enfrentar o feiticeiro e descobrir a verdade sobre seu passado.', 0x00) 
insert tblGeneroProduto values	(1,3),
								(2,3)


insert tblProduto values (0, 'O Iluminado', 'Stephen King', '2012', 0, 0, 0, 'Livro', 'Suma', 'O romance, magistralmente levado ao cinema por Stanley Kubrick, continua apaixonando (e aterrorizando) novas gera��es de leitores. A luta assustadora entre dois mundos. Um menino e o desejo assassino de poderosas for�as malignas. Uma fam�lia ref�m do mal. Nesta guerra sem testemunhas, vencer� o mais forte. Danny Torrance n�o � um menino comum. � capaz de ouvir pensamentos e transportar-se no tempo. Danny � iluminado. Ser� uma maldi��o ou uma b�n��o? A resposta pode estar guardada na impon�ncia assustadora do hotel Overlook. Em O iluminado, quando Jack Torrance consegue o emprego de zelador no velho hotel, todos os problemas da fam�lia parecem estar solucionados. N�o mais o desemprego e as noites de bebedeiras. N�o mais o sofrimento da esposa, Wendy. Tranquilidade e ar puro para o pequeno Danny livrar-se das convuls�es que assustam a fam�lia. S� que o Overlook n�o � um hotel comum. O tempo esqueceu-se de enterrar velhos �dios e de cicatrizar antigas feridas, e esp�ritos malignos ainda residem nos corredores. O hotel � uma chaga aberta de ressentimento e desejo de vingan�a. � uma senten�a de morte. E somente os poderes de Danny podem fazer frente � dissemina��o do mal.
', 0x00)
insert tblGeneroProduto values	(5,4),
								(6,4)

insert tblProduto values (0, 'Anjos', 'Padre Marcelo Rossi', '2002', 0, 0, 0, 'CD', 'Mercury', 'Can��es para um novo mil�nio', 0x00)
insert tblGeneroProduto values	(7,5)

insert tblProduto values (0, 'Emoji: O Filme', 'Tony Leondis', '2017', 0, 0, 0, 'DVD', 'Sony Pictures', 'Textopolis � a cidade onde os Emojis favoritos dos usu�rios de smartphones vivem e trabalham. L�, todos eles vivem em fun��o de um sonho: serem usados nos textos dos humanos. Todos est�o acostumados a ter somente uma express�o facial - com exce��o de Gene, que nasceu com um bug em seu sistema, que o permite trocar de rosto atrav�s de um filtro especial. Determinado � se tornar um emoji normal como todos os outros, eles vai encarar uma jornada fant�sticas atrav�s dos aplicativos de celular mais populares desta gera��o - e no meio do caminho, claro, fazer novos amigos.', 0x00)
insert tblGeneroProduto values	(8,6)

insert tblProduto values (0, 'Steven Universe: Art & Origins', 'Chris McDonnell', '2017', 0, 0, 0, 'Livro', 'Abrams', 'Steven Universe: Art & Origins is the first book to take fans behind the scenes of the groundbreaking and boundlessly creative Cartoon Network animated series Steven Universe. The eponymous Steven is a boy who�alongside his mentors, the Crystal Gems (Garnet, Amethyst, and Pearl)�must learn to use his inherited powers to protect his home, Beach City, from the forces of evil. Bursting with concept art, production samples, early sketches, storyboards, and exclusive commentary, this lavishly illustrated companion book offers a meticulous written and visual history of the show, as well as an all-access tour of the creative team�s process. Steven Universe: Art & Origins reveals how creator Rebecca Sugar, the writers, the animators, and the voice actors work in tandem to bring this adventure-packed television series to life.', 0x00)
insert tblGeneroProduto values	(9,7)


insert tblProduto values (0, 'Little Witch Academia - Vol. 1', 'Yoh Yoshinari, Keisuke Sato', '2018', 0, 0, 0, 'Revista', 'TRIGGER', '�Basta estender a m�o, que vai come�ar a minha hist�ria�!!� Atsuko Kagari se matriculou na Escola de Magia Luna Nova, especializada em treinar bruxas, devido sua admira��o pela bruxa Shiny Chariot. Ela tem grandes expectativas quanto a essa sua nova vida escolar, cheia de novas descobertas� Baseada no anim� de sucesso da Netflix, come�a agora uma hist�ria de magia e fantasia, cheia de encontros, ensinamentos e amadurecimento! ', 0x00)
insert tblGeneroProduto values	(10,8),
								(1,8),
								(2,8)


insert tblProduto values (0, 'Destinos e F�rias', 'Lauren Groff', '2016', 0, 0, 0, 'Livro', 'Intr�seca', 'Toda hist�ria tem dois lados. Todo relacionamento tem duas perspectivas. E �s vezes a chave para um grande casamento n�o est� em suas verdades, mas em seus segredos.
Aos 22 anos, Lotto e Mathilde s�o jovens, perdidamente apaixonados e destinados ao sucesso. Eles se conhecem nos �ltimos meses da faculdade e antes da formatura j� est�o casados. Seguem-se anos dif�ceis, mas rom�nticos: reuni�es com amigos no apartamento em Manhattan; uma carreira que ainda n�o paga as contas; uma casa onde s� cabem felicidade e sexo bom. Uma d�cada depois, o caminho tornou-se mais s�lido. Ele � um dramaturgo famoso e ela se dedica integralmente ao sucesso do marido. A vida dos dois � invejada como a verdadeira defini��o de parceria bem-sucedida.
Por�m, nem tudo � o que parece; toda hist�ria tem dois lados, e em um casamento essa m�xima se faz ainda mais verdadeira. Se em �Destinos� somos seduzidos pela imagem do casal perfeito, em �F�rias� a tempestuosa raiva de Mathilde se revela fervendo sob a superf�cie. Em uma reviravolta emocionalmente complexa, o que come�ou como uma ode a uma uni�o extraordin�ria se torna muito mais.
Com profundidade e um emaranhado de tramas, a prosa vibrante e original de Destinos e f�rias comove, provoca e surpreende. Um romance sobre os muitos casamentos poss�veis entre o amor, a arte e o poder e sobre os diferentes pontos de vista pelos quais essas combina��es podem ser enxergadas.', 0x00)
insert tblGeneroProduto values	(3,9),
								(11,9)


insert tblProduto values (0, 'Aerosmith', 'Aerosmith', '1973', 0, 0, 0, 'CD', 'Sbme Special Mkts.', 'Aerosmith � o �lbum de estreia da banda de rock estadunidense Aerosmith, lan�ado em 5 de janeiro de 1973. O �lbum foi gravado em duas semanas na Intermedia Studio em Boston, Massachusetts. Boa parte do disco � fortemente influenciado pelo blues. A can��o "Walkin the Dog" � um cover originalmente realizada por Rufus Thomas. Tamb�m est� no �lbum a can��o "Dream On", que se tornou um single top dez americano quando re-lan�ada em 1976. "Dream On" foi lan�ada primeiro como single em 1973, o �lbum alcan�ou a posi��o #21 na Billboard 200.', 0x00)
insert tblGeneroProduto values	(12,10)

update tblProduto set ImagemProd=(select top 1 BulkColumn as img from openrowset(bulk N'C:\Users\CakeIsALie\Pictures\23015585.jpg', SINGLE_BLOB) as _)


select L.IDLeitor, L.Nome, L.CPF, L.RG, L.DataNasc, L.Endereco, L.Telefone, L.Email, L.DataCadastro, TL.TipoLeitor, L.IDTipoLeitor from tblLeitor as L join tblTipoLeitor as TL on L.IDTipoLeitor = TL.IDTipoLeitor

select * from tblProduto