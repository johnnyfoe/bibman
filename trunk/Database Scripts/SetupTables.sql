
    if exists (select * from dbo.sysobjects where id = object_id(N'[Publication]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [Publication]

    create table [Publication] (
        Id INT IDENTITY NOT NULL,
       CiteKey NVARCHAR(255) null,
       Owner NVARCHAR(255) null,
       EntryType NVARCHAR(255) null,
       Abstract NVARCHAR(255) null,
       Address NVARCHAR(255) null,
       Annote NVARCHAR(255) null,
       Authors NVARCHAR(255) null,
       Booktitle NVARCHAR(255) null,
       Chapter NVARCHAR(255) null,
       Crossref NVARCHAR(255) null,
       Edition NVARCHAR(255) null,
       Editors NVARCHAR(255) null,
       Howpublished NVARCHAR(255) null,
       Institution NVARCHAR(255) null,
       Journal NVARCHAR(255) null,
       TheKey NVARCHAR(255) null,
       Month NVARCHAR(255) null,
       Note NVARCHAR(255) null,
       Number NVARCHAR(255) null,
       Organization NVARCHAR(255) null,
       Pages NVARCHAR(255) null,
       Publisher NVARCHAR(255) null,
       School NVARCHAR(255) null,
       Series NVARCHAR(255) null,
       Title NVARCHAR(255) null,
       Type NVARCHAR(255) null,
       Volume NVARCHAR(255) null,
       Year NVARCHAR(255) null,
       DeletionTime DATETIME null,
       AmendmentTime DATETIME null,
       CreationTime DATETIME null,
       primary key (Id)
    )
