CREATE TABLE [dbo].[Frame_RemotingObject] (
    [ObjectType]    VARCHAR (500) NOT NULL,
    [URI]           VARCHAR (500) NOT NULL,
    [objectDll]     VARCHAR (500) NULL,
    [interfaceType] VARCHAR (500) NOT NULL,
    [interfaceDll]  VARCHAR (500) NULL,
    CONSTRAINT [PK_Frame_RemotingObject] PRIMARY KEY CLUSTERED ([ObjectType] ASC, [URI] ASC, [interfaceType] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'接口类型对应库', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Frame_RemotingObject', @level2type = N'COLUMN', @level2name = N'interfaceDll';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'接口类型', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Frame_RemotingObject', @level2type = N'COLUMN', @level2name = N'interfaceType';

