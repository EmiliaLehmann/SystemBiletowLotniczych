namespace SystemBiletowLotniczych.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bilets",
                c => new
                    {
                        BiletId = c.Int(nullable: false, identity: true),
                        ImiePasazera = c.String(),
                        NazwiskoPasazera = c.String(),
                        DataWylotu = c.DateTime(nullable: false),
                        Cena = c.Double(nullable: false),
                        Klasa = c.Int(nullable: false),
                        DataRezerwacji = c.DateTime(nullable: false),
                        MiastoWylotu = c.String(),
                        MiastoPrzylotu = c.String(),
                        NumerLotu = c.String(),
                        NumerMiejsca = c.Int(nullable: false),
                        StawkaPodatkowa = c.Double(),
                        WizaWymagana = c.Boolean(),
                        CenaUslugDodatkowych = c.Double(),
                        DodatkoweOplaty = c.Double(),
                        MiastoKoncowe = c.String(),
                        Znizka = c.Double(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        BiletZPrzesiadkami_BiletId = c.Int(),
                    })
                .PrimaryKey(t => t.BiletId)
                .ForeignKey("dbo.Bilets", t => t.BiletZPrzesiadkami_BiletId)
                .Index(t => t.BiletZPrzesiadkami_BiletId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bilets", "BiletZPrzesiadkami_BiletId", "dbo.Bilets");
            DropIndex("dbo.Bilets", new[] { "BiletZPrzesiadkami_BiletId" });
            DropTable("dbo.Bilets");
        }
    }
}
