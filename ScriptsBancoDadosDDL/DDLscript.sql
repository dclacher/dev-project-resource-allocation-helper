CREATE TABLE Usuario (
usu_id int PRIMARY KEY IDENTITY(1,1) NOT NULL,
usu_nome varchar(50) NOT NULL,
usu_sobrenome varchar(50) NOT NULL,
usu_nome_usuario varchar(30) NOT NULL,
usu_senha varchar(200) NOT NULL,
usu_tipo_id int NOT NULL,
usu_email varchar(200) NOT NULL,
usu_salt varchar(50) NOT NULL
)

CREATE TABLE Tipo_Usuario (
tipo_id int PRIMARY KEY IDENTITY(1,1) NOT NULL,
tipo_nome varchar(20) NOT NULL,
tipo_descricao varchar(200)
)

CREATE TABLE Projeto (
proj_id int PRIMARY KEY IDENTITY(1,1) NOT NULL,
proj_nome varchar(50) NOT NULL,
proj_descricao varchar(200),
proj_data_inicio datetime NOT NULL,
proj_data_termino datetime NOT NULL,
proj_status varchar(20) NOT NULL,
proj_prioridade varchar(20) NOT NULL,
proj_usu_id int NOT NULL,
proj_avaliado bit NOT NULL DEFAULT(0),
FOREIGN KEY(proj_usu_id) REFERENCES Usuario (usu_id)
)

CREATE TABLE Projeto_Funcionario (
prfu_id int PRIMARY KEY IDENTITY(1,1) NOT NULL,
prfu_data_inicio datetime NOT NULL,
prfu_data_termino datetime NOT NULL,
prfu_usu_id int NOT NULL,
prfu_proj_id int NOT NULL,
FOREIGN KEY(prfu_usu_id) REFERENCES Usuario (usu_id),
FOREIGN KEY(prfu_proj_id) REFERENCES Projeto (proj_id)
)

CREATE TABLE Projeto_Competencia (
prco_proj_id int NOT NULL,
prco_comp_id int NOT NULL,
PRIMARY KEY(prco_proj_id,prco_comp_id),
FOREIGN KEY(prco_proj_id) REFERENCES Projeto (proj_id)
)

CREATE TABLE Competencia (
comp_id int PRIMARY KEY IDENTITY(1,1) NOT NULL,
comp_nome varchar(50) NOT NULL,
comp_descricao varchar(200),
comp_tcom_id int NOT NULL
)

CREATE TABLE Avaliacao (
aval_id int PRIMARY KEY IDENTITY(1,1) NOT NULL,
aval_nivel numeric(4) NOT NULL,
aval_data datetime NOT NULL,
aval_usu_id int NOT NULL,
aval_atual BIT NOT NULL DEFAULT(0), 
FOREIGN KEY(aval_usu_id) REFERENCES Usuario (usu_id)
)

CREATE TABLE Usu_Aval_Comp (
uac_usu_id int NOT NULL,
uac_aval_id int NOT NULL,
uac_comp_id int NOT NULL,
PRIMARY KEY(uac_usu_id,uac_aval_id,uac_comp_id),
FOREIGN KEY(uac_usu_id) REFERENCES Usuario (usu_id),
FOREIGN KEY(uac_aval_id) REFERENCES Avaliacao (aval_id),
FOREIGN KEY(uac_comp_id) REFERENCES Competencia (comp_id)
)

CREATE TABLE Tipo_Competencia (
tcom_id int PRIMARY KEY IDENTITY(1,1) NOT NULL,
tcom_nome varchar(20) NOT NULL
)

ALTER TABLE Usuario ADD FOREIGN KEY(usu_tipo_id) REFERENCES Tipo_Usuario (tipo_id)
ALTER TABLE Projeto_Competencia ADD FOREIGN KEY(prco_comp_id) REFERENCES Competencia (comp_id)
ALTER TABLE Competencia ADD FOREIGN KEY(comp_tcom_id) REFERENCES Tipo_Competencia (tcom_id)
