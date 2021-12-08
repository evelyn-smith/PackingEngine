//---------------------------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated by T4Model template for T4 (https://github.com/linq2db/linq2db).
//    Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//---------------------------------------------------------------------------------------------------

#pragma warning disable 1591

using System;
using System.Linq;

using LinqToDB;
using LinqToDB.Mapping;

namespace PackingService.Sql.Services.Models
{
	/// <summary>
	/// Database       : PackingEngine
	/// Data Source    : tcp:capstonepackingengine.database.windows.net,1433
	/// Server Version : 12.00.2255
	/// </summary>
	public partial class PackingEngineDB : LinqToDB.Data.DataConnection
	{
		public ITable<Boxdata>              Boxdatas              { get { return this.GetTable<Boxdata>(); } }
		public ITable<BoxdataCarousel>      BoxdataCarousels      { get { return this.GetTable<BoxdataCarousel>(); } }
		public ITable<BoxdataPallet>        BoxdataPallets        { get { return this.GetTable<BoxdataPallet>(); } }
		public ITable<DatabaseFirewallRule> DatabaseFirewallRules { get { return this.GetTable<DatabaseFirewallRule>(); } }
		public ITable<ItemsToPalletize>     ItemsToPalletizes     { get { return this.GetTable<ItemsToPalletize>(); } }
		public ITable<OrderDetail>          OrderDetails          { get { return this.GetTable<OrderDetail>(); } }
		public ITable<OrderResult>          OrderResults          { get { return this.GetTable<OrderResult>(); } }
		public ITable<OrderStatus>          OrderStatus           { get { return this.GetTable<OrderStatus>(); } }

		public PackingEngineDB()
		{
			InitDataContext();
			InitMappingSchema();
		}

		public PackingEngineDB(string configuration)
			: base(configuration)
		{
			InitDataContext();
			InitMappingSchema();
		}

		partial void InitDataContext  ();
		partial void InitMappingSchema();
	}

	[Table(Schema="dbo", Name="boxdata")]
	public partial class Boxdata
	{
		[Column(),              NotNull    ] public string  Name          { get; set; } // varchar(50)
		[Column(),              NotNull    ] public double  Length        { get; set; } // float
		[Column(),              NotNull    ] public double  Width         { get; set; } // float
		[Column(),              NotNull    ] public double  Height        { get; set; } // float
		[Column(),              NotNull    ] public double  MaxWeight     { get; set; } // float
		[Column(),                 Nullable] public double? Weight        { get; set; } // float
		[Column("COST_FACTOR"),    Nullable] public double? CostFactor    { get; set; } // float
		[Column(),              NotNull    ] public bool    CanBeFlagpole { get; set; } // bit
	}

	[Table(Schema="dbo", Name="boxdata_Carousel")]
	public partial class BoxdataCarousel
	{
		[Column(),              NotNull    ] public string  Name       { get; set; } // varchar(50)
		[Column(),              NotNull    ] public double  Length     { get; set; } // float
		[Column(),              NotNull    ] public double  Width      { get; set; } // float
		[Column(),              NotNull    ] public double  Height     { get; set; } // float
		[Column(),              NotNull    ] public double  MaxWeight  { get; set; } // float
		[Column(),                 Nullable] public double? Weight     { get; set; } // float
		[Column("COST_FACTOR"),    Nullable] public double? CostFactor { get; set; } // float
	}

	[Table(Schema="dbo", Name="boxdata_Pallet")]
	public partial class BoxdataPallet
	{
		[Column(),              NotNull    ] public string  Name       { get; set; } // varchar(50)
		[Column(),              NotNull    ] public double  Length     { get; set; } // float
		[Column(),              NotNull    ] public double  Width      { get; set; } // float
		[Column(),              NotNull    ] public double  Height     { get; set; } // float
		[Column(),              NotNull    ] public double  MaxWeight  { get; set; } // float
		[Column(),                 Nullable] public double? Weight     { get; set; } // float
		[Column("COST_FACTOR"),    Nullable] public double? CostFactor { get; set; } // float
	}

	[Table(Schema="sys", Name="database_firewall_rules", IsView=true)]
	public partial class DatabaseFirewallRule
	{
		[Column("id"),               Identity] public int      Id             { get; set; } // int
		[Column("name"),             NotNull ] public string   Name           { get; set; } // nvarchar(128)
		[Column("start_ip_address"), NotNull ] public string   StartIpAddress { get; set; } // varchar(45)
		[Column("end_ip_address"),   NotNull ] public string   EndIpAddress   { get; set; } // varchar(45)
		[Column("create_date"),      NotNull ] public DateTime CreateDate     { get; set; } // datetime
		[Column("modify_date"),      NotNull ] public DateTime ModifyDate     { get; set; } // datetime
	}

	[Table(Schema="dbo", Name="ItemsToPalletize")]
	public partial class ItemsToPalletize
	{
		[Column,     NotNull    ] public string  CustOrderNo   { get; set; } // varchar(6)
		[Column,     NotNull    ] public string  Alias         { get; set; } // varchar(20)
		[Column,     NotNull    ] public int     QTY           { get; set; } // int
		[Column,     NotNull    ] public string  BoxNumber     { get; set; } // varchar(21)
		[Column,     NotNull    ] public string  BoxName       { get; set; } // varchar(20)
		[Column,     NotNull    ] public decimal BoxLength     { get; set; } // decimal(12, 2)
		[Column,     NotNull    ] public decimal BoxWidth      { get; set; } // decimal(12, 2)
		[Column,     NotNull    ] public decimal BoxHeight     { get; set; } // decimal(12, 2)
		[Column,     NotNull    ] public decimal BoxWeight     { get; set; } // decimal(12, 2)
		[Column,        Nullable] public int?    PalletNumber  { get; set; } // int
		[Column,        Nullable] public string  PalletName    { get; set; } // varchar(50)
		[Column,        Nullable] public string  OSCOStatus    { get; set; } // varchar(50)
		[PrimaryKey, Identity   ] public int     ITPID         { get; set; } // int
		[Column,     NotNull    ] public bool    CanBeFlagpole { get; set; } // bit
		[Column,     NotNull    ] public char    CharWeight    { get; set; } // char(1)
		[Column,     NotNull    ] public bool    AbnormalShape { get; set; } // bit
	}

	[Table(Schema="dbo", Name="OrderDetails")]
	public partial class OrderDetail
	{
		[Column(),                    NotNull              ] public string  OrderId         { get; set; } // varchar(50)
		[Column(),                    NotNull              ] public string  Sku             { get; set; } // varchar(50)
		[Column(),                    NotNull              ] public int     Quantity        { get; set; } // int
		[Column(),                    NotNull              ] public double  Lenght          { get; set; } // float
		[Column(),                    NotNull              ] public double  Width           { get; set; } // float
		[Column(),                    NotNull              ] public double  Height          { get; set; } // float
		[Column(),                    NotNull              ] public double  Weight          { get; set; } // float
		[Column("NO_LOAD"),              Nullable          ] public bool?   NoLoad          { get; set; } // bit
		[Column("LOAD_DIRECTION"),       Nullable          ] public string  LoadDirection   { get; set; } // varchar(50)
		[Column("NEST_ORIENTATION"),     Nullable          ] public string  NestOrientation { get; set; } // varchar(50)
		[Column("NEST_DIMENSION"),       Nullable          ] public double? NestDimension   { get; set; } // float
		[Column("NEST_CLASS"),           Nullable          ] public string  NestClass       { get; set; } // varchar(50)
		[Column("MIX_CLASS_ID"),         Nullable          ] public int?    MixClassId      { get; set; } // int
		[Column("MIX_CLASS_OPTIONS"),    Nullable          ] public bool?   MixClassOptions { get; set; } // bit
		[Column("KEEP_TOGETHER"),        Nullable          ] public bool?   KeepTogether    { get; set; } // bit
		[Column(),                       Nullable          ] public string  SHAPE           { get; set; } // varchar(32)
		[Column(),                       Nullable          ] public double? Length2         { get; set; } // float
		[Column(),                       Nullable          ] public double? Width2          { get; set; } // float
		[Column(),                       Nullable          ] public double? Height2         { get; set; } // float
		[Column(),                    PrimaryKey,  Identity] public int     OrderDetailsID  { get; set; } // int
		[Column("PACK_IN_QTY"),          Nullable          ] public int?    PackInQty       { get; set; } // int
	}

	[Table(Schema="dbo", Name="OrderResults")]
	public partial class OrderResult
	{
		[Column,     NotNull    ] public string  OrderId        { get; set; } // varchar(50)
		[Column,     NotNull    ] public string  SkuName        { get; set; } // varchar(50)
		[Column,        Nullable] public int?    SkuQty         { get; set; } // int
		[Column,        Nullable] public int?    BoxNumber      { get; set; } // int
		[Column,        Nullable] public string  BoxName        { get; set; } // varchar(50)
		[Column,     NotNull    ] public string  Status         { get; set; } // varchar(50)
		[Column,        Nullable] public double? TotalWeight    { get; set; } // float
		[Column,        Nullable] public double? TotalSkuVol    { get; set; } // float
		[Column,        Nullable] public double? BoxLength      { get; set; } // float
		[Column,        Nullable] public double? BoxWidth       { get; set; } // float
		[Column,        Nullable] public double? BoxHeight      { get; set; } // float
		[Column,        Nullable] public double? BoxVolume      { get; set; } // float
		[Column,        Nullable] public double? DimWeight      { get; set; } // float
		[Column,        Nullable] public double? CubeUtil       { get; set; } // float
		[Column,     NotNull    ] public decimal ShipmentCharge { get; set; } // decimal(12, 2)
		[PrimaryKey, Identity   ] public int     OrderResultsID { get; set; } // int
	}

	[Table(Schema="dbo", Name="OrderStatus")]
	public partial class OrderStatus
	{
		[PrimaryKey, NotNull    ] public string OrderId             { get; set; } // varchar(50)
		[Column,     NotNull    ] public double MaxCubeUtil         { get; set; } // float
		[Column,     NotNull    ] public string Status              { get; set; } // varchar(50)
		[Column,     NotNull    ] public string BoxTableName        { get; set; } // varchar(50)
		[Column,        Nullable] public string SpecialBoxTableName { get; set; } // varchar(50)
		[Column,        Nullable] public string Message             { get; set; } // varchar(256)
		[Column,        Nullable] public bool?  DIMOptimize         { get; set; } // bit
		[Column,        Nullable] public int?   DIMFactor           { get; set; } // int
		[Column,        Nullable] public int?   DIMVolume           { get; set; } // int
		[Column,        Nullable] public bool?  AutoBox             { get; set; } // bit
	}

	public static partial class TableExtensions
	{
		public static ItemsToPalletize Find(this ITable<ItemsToPalletize> table, int ITPID)
		{
			return table.FirstOrDefault(t =>
				t.ITPID == ITPID);
		}

		public static OrderDetail Find(this ITable<OrderDetail> table, int OrderDetailsID)
		{
			return table.FirstOrDefault(t =>
				t.OrderDetailsID == OrderDetailsID);
		}

		public static OrderResult Find(this ITable<OrderResult> table, int OrderResultsID)
		{
			return table.FirstOrDefault(t =>
				t.OrderResultsID == OrderResultsID);
		}

		public static OrderStatus Find(this ITable<OrderStatus> table, string OrderId)
		{
			return table.FirstOrDefault(t =>
				t.OrderId == OrderId);
		}
	}
}

#pragma warning restore 1591
