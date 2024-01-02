namespace Odb.Client.Lib
{
    public enum BoardSide
    {
        BsNone,
        Top,
        Bottom
    }

    public enum LineShape
    {
        Square,
		Round
    };

    public enum Polarity
    {
        Positive,
		Negative
    };

    public enum UnitType
    {
        None,
		Metric,
		Imperial
    };

    public class FileArchive
    {
        public StepDirectory.StringDictionary stepsByName { get; set; }
        public MiscInfoFile miscInfoFile { get; set; }
        public Matrixfile matrixFile { get; set; }
        public StandardFontsFile standardFontsFile { get; set; }        
        public SymbolsDirectory.StringDictionary symbolsDirectoriesByName { get; set; }
    }    

    public class StepDirectory
    {
        public string name { get; set; }
        public string path { get; set; }
        public LayerDirectory.StringDictionary layersByName { get; set; }
        public NetlistFile.StringDictionary netlistsByName { get; set; }
        public EdaDataFile edadatafile { get; set; }
        public AttrListFile attrlistfile { get; set; }

        public class StringDictionary : Dictionary<string, StepDirectory>   {}
    }

    public class LayerDirectory
    {
        public string name { get; set; }
        public string path { get; set; }
        public ComponentsFile components { get; set; }
        public AttrListFile attrlistFile { get; set; }
        public FeaturesFile featureFile { get; set; }

        public class StringDictionary : Dictionary<string, LayerDirectory> { }
    }     

    public class AttrListFile
    {
        public string directory { get; set; }
        public string path { get; set; }
        public string units { get; set; }
        public Dictionary<string, string> attributesByName { get; set; }
    }          

    public class FeaturesFile
    {
        public string units { get; set; }
        public uint id { get; set; }
        public int numFeatures { get; set; }
        public FeatureRecord[] featureRecords { get; set; }
    }

    public class FeatureRecord
    {
        public enum Type
        {
            Arc,
				Pad,
				Surface,
				Barcode,
				Text,
				Line
        };

        public Type type { get; set; }
        public float xs { get; set; }
        public float ys { get; set; }
        public float xe { get; set; }
        public float ye { get; set; }
        public float x { get; set; }
        public float y { get; set; }
        public int aptDefSymbolNum { get; set; }
        public float aptDefResizeFactor { get; set; }
        public float xc { get; set; }
        public float yc { get; set; }
        public bool cw { get; set; }
        public string font { get; set; }
        public float xsize { get; set; }
        public float ysize { get; set; }
        public float widthFactor { get; set; }
        public string text { get; set; }
        public int version { get; set; }
        public uint symNum { get; set; }
        public Polarity polarity { get; set; }
        public int dcode { get; set; }
        public uint id { get; set; }
        public int orientDef { get; set; }
        public int orientDefRotation { get; set; }
        public ContourPolygon[] contourPolygons { get; set; }
    }

    public class ContourPolygon
    {
        public enum Type
        {
            Island,
			Hole
        };

        public Type type { get; set; }
        public float xStart { get; set; }
        public float yStart { get; set; }
        public PolygonPart[] polygonParts { get; set; }
    }

    public class PolygonPart
    {
        public enum Type
        {
            Segment,
            Arc
        }

        public float endX { get; set; }
        public float endY { get; set; }
        public float xCenter { get; set; }
        public float yCenter { get; set; }
        public bool isClockwise { get; set; }
    }    

    public class ComponentsFile
    {
        public string units { get; set; }
        public uint id { get; set; }
        public BoardSide side { get; set; }
        public string layerName { get; set; }
        public string path { get; set; }
        public string directory { get; set; }
        public string[] attributeNames { get; set; }
        public string[] attributeTextValues { get; set; }
        public ComponentRecord[] componentRecords { get; set; }
    }

    public class ComponentRecord
    {
        public int pkgRef { get; set; }
        public float locationX { get; set; }
        public float locationY { get; set; }
        public int rotation { get; set; }
        public bool mirror { get; set; }
        public string compName { get; set; }
        public string partName { get; set; }
        public string attributes { get; set; }
        public int index { get; set; }
        public PropertyRecord[] propertyRecords { get; set; }
        public ToeprintRecord[] toeprintRecords { get; set; }
    }

    public class PropertyRecord
    {
        public string name { get; set; }
        public string value { get; set; }
        public float[] floatValues { get; set; }
    }

    public class ToeprintRecord
    {
        public uint pinNumber { get; set; }
        public float locationX { get; set; }
        public float locationY { get; set; }
        public float rotation { get; set; }
        public bool mirror { get; set; }
        public uint netNumber { get; set; }
        public uint subnetNumber { get; set; }
        public string name { get; set; }
    }        

    public class NetlistFile
    {
        public enum Staggered
        {
            Yes,
			No,
			Unknown
        };

        public string path { get; set; }
        public string name { get; set; }
        public string units { get; set; }
        public bool optimized { get; set; }
        public Staggered staggered { get; set; }
        public NetName[] netNames { get; set; }
        public NetName.StringDictionary netRecordsByName { get; set; }
        public NetPointRecord[] netPointRecords { get; set; }

        public class StringDictionary : Dictionary<string, NetlistFile> { }

    }     

    public class NetName
    {
        public uint serialNumber { get; set; }
        public string netName { get; set; }

        public class StringDictionary : Dictionary<string, NetName> { }
    }

    public class NetPointRecord
    {
        public uint netNumber { get; set; }
        public float radius { get; set; }
        public float x { get; set; }
        public float y { get; set; }
        public string side { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string epoint { get; set; }
        public string exp { get; set; }
        public bool commentPoint { get; set; }
        public int staggeredX { get; set; }
        public int staggeredY { get; set; }
        public int staggeredRadius { get; set; }
        public int viaPoint { get; set; }
        public int fiducialPoint { get; set; }
        public int testPoint { get; set; }
        public string testExecutionSide { get; set; }
    }

    public class EdaDataFile
    {
        public string path { get; set; }
        public string units { get; set; }
        public string source { get; set; }
        public string[] layerNames { get; set; }
        public string[] attributeNames { get; set; }
        public string[] attributeTextValues { get; set; }
        public NetRecord[] netRecords { get; set; }
        public NetRecord.StringDictionary netRecordsByName { get; set; }
        public PackageRecord[] packageRecords { get; set; }
        public PackageRecord.StringDictionary packageRecordsByName { get; set; }
    }    

    public class NetRecord
    {
        public string name { get; set; }
        public string attributesIdString { get; set; }
        public uint index { get; set; }
        public SubnetRecord[] subnetRecords { get; set; }

        public class StringDictionary : Dictionary<string, NetRecord> { }
    }

    public class SubnetRecord
    {
        public enum Type
        {
            Via,
            Trace,
            Plane,
            Toeprint
        };
        public enum FillType
        {
            Solid,
            Outline
        };

        public enum CutoutType
        {
            Circle,
            Rectangle,
            Octagon,
            Exact
        };

        public Type type { get; set; }
        public FeatureIdRecord[] featureIdRecords { get; set; }
        public BoardSide side { get; set; }
        public uint componentNumber { get; set; }
        public uint toeprintNumber { get; set; }
    }

    public class FeatureIdRecord
    {
        public enum Type
        {
            Copper,
			Laminate,
			Hole
        };

        public Type type { get; set; }
        public uint layerNumber { get; set; }
        public uint featureNumber { get; set; }
    }
    
    public class PackageRecord
    {
        public string name { get; set; }
        public float pitch { get; set; }
        public float xMin { get; set; }
        public float yMin { get; set; }
        public float xMax { get; set; }
        public float yMax { get; set; }
        public string attributesIdString { get; set; }
        public PinRecord[] pinRecords { get; set; }
        public OutlineRecord[] outlineRecords { get; set; }

        public class StringDictionary : Dictionary<string, PackageRecord> { }
    }

    public class PinRecord
    {
        public enum Type
        {
            ThroughHole,
            Blind,
            Surface
        };

        public enum ElectricalType
        {
            Electrical,
            NonElectrical,
            Undefined
        };

        public enum MountType
        {
            Smt,
            RecommendedSmtPad,
            MT_ThroughHole,
            RecommendedThroughHole,
            Pressfit,
            NonBoard,
            Hole,
            MT_Undefined    // default
        };

        public string name { get; set; }
        public Type type { get; set; }
        public float xCenter { get; set; }
        public float yCenter { get; set; }
        public int finishedHoleSize { get; set; }
        public ElectricalType electricalType { get; set; }
        public MountType mountType { get; set; }
        public uint id { get; set; }
        public uint index { get; set; }
    }

    public class OutlineRecord
    {
        public enum Type
        {
            Rectangle,
            Circle,
            Square,
            Contour
        };

        public Type type { get; set; }
        public float lowerLeftX { get; set; }
        public float lowerLeftY { get; set; }
        public float width { get; set; }
        public float height { get; set; }
        public float xCenter { get; set; }
        public float yCenter { get; set; }
        public float halfSide { get; set; }
        public float radius { get; set; }
        public ContourPolygon[] contourPolygons { get; set; }
    }  

    public class MiscInfoFile
    {
        public string productModelName { get; set; }
        public string jobName { get; set; }
        public string odbVersionMajor { get; set; }
        public string odbVersionMinor { get; set; }
        public string odbSource { get; set; }
        public DateTime creationDateDate { get; set; }
        public DateTime saveDate { get; set; }
        public string saveApp { get; set; }
        public string saveUser { get; set; }
        public string units { get; set; }
        public uint maxUniqueId { get; set; }
    }

    public class Matrixfile
    {
        public Step[] steps { get; set; }
        public Layer[] layers { get; set; }
    }

    public class Step
    {
        public uint column { get; set; }
        public string name { get; set; }
    }

    public class Layer
    {
        public enum Type
        {
            Signal,
            PowerGround,
            Dielectric,
            Mixed,
            SolderMask,
            SolderPaste,
            SilkScreen,
            Drill,
            Rout,
            Document,
            Component,
            Mask,
            ConductivePaste
        };

        public enum Context
        {
            Board,
            Misc
        };

        public enum DielectricType
        {
            None,
            Prepreg,
            Core
        };

        public enum Form
        {
            Rigid,
            Flex
        };

        public uint row { get; set; }
        public Type type { get; set; }
        public string name { get; set; }
        public long cuTop { get; set; }
        public long cuBottom { get; set; }
        public long _ref { get; set; }
        public uint id { get; set; }
        public string startName { get; set; }
        public string endName { get; set; }
        public Context context { get; set; }
    }

    public class StandardFontsFile
    {
        public float xSize { get; set; }
        public float ySize { get; set; }
        public float offset { get; set; }
        public CharacterBlock[] mCharacterBlocks { get; set; }
    }

    public class CharacterBlock
    {
        public string character { get; set; }
        public LineRecord[] mLineRecords { get; set; }
    }

    public class LineRecord
    {
        public float xStart { get; set; }
        public float yStart { get; set; }
        public float xEnd { get; set; }
        public float yEnd { get; set; }
        public Polarity polarity { get; set; }
        public LineShape shape { get; set; }
        public float width { get; set; }
    }

    public class SymbolsDirectory
    {
        public string name { get; set; }
        public string path { get; set; }
        public AttrListFile attrlistFile { get; set; }
        public FeaturesFile featureFile { get; set; }

        public class StringDictionary : Dictionary<string, SymbolsDirectory> { }
    }
}
