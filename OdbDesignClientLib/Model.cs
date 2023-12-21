namespace Odb.Client.Lib
{
    public class FileArchive
    {
        public Dictionary<string, StepDirectory> stepsByName { get; set; }
        public MiscInfoFile miscInfoFile { get; set; }
        public Matrixfile matrixFile { get; set; }
        public StandardFontsFile standardFontsFile { get; set; }
        public Dictionary<string, SymbolsDirectory> symbolsDirectoriesByName { get; set; }
    }    

    public class StepDirectory
    {
        public string name { get; set; }
        public string path { get; set; }
        public Dictionary<string, LayerDirectory> layersByName { get; set; }
        public Dictionary<string, NetlistFile> netlistsByName { get; set; }
        public EdaDataFile edadatafile { get; set; }
        public AttrListFile attrlistfile { get; set; }
    }

    public class LayerDirectory
    {
        public string name { get; set; }
        public string path { get; set; }
        public ComponentsFile components { get; set; }
        public AttrListFile attrlistFile { get; set; }
        public FeaturesFile featureFile { get; set; }
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
        public string type { get; set; }
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
        public string polarity { get; set; }
        public int dcode { get; set; }
        public uint id { get; set; }
        public int orientDef { get; set; }
        public int orientDefRotation { get; set; }
        public ContourPolygon[] contourPolygons { get; set; }
    }

    public class ContourPolygon
    {
        public string type { get; set; }
        public float xStart { get; set; }
        public float yStart { get; set; }
        public PolygonPart[] polygonParts { get; set; }
    }

    public class PolygonPart
    {
        public float endX { get; set; }
        public float endY { get; set; }
        public float xCenter { get; set; }
        public float yCenter { get; set; }
        public bool isClockwise { get; set; }
    }

    public enum BoardSide
    {
        NotApplicable,
        Top,
        Bottom
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
        public string path { get; set; }
        public string name { get; set; }
        public string units { get; set; }
        public bool optimized { get; set; }
        public string staggered { get; set; }
        public NetName[] netNames { get; set; }
        public Dictionary<string, NetName> netRecordsByName { get; set; }
        public NetPointRecord[] netPointRecords { get; set; }
    }     

    public class NetName
    {
        public uint serialNumber { get; set; }
        public string netName { get; set; }
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
        public Dictionary<string, NetRecord> netRecordsByName { get; set; }
        public PackageRecord[] packageRecords { get; set; }
        public Dictionary<string, PackageRecord> packageRecordsByName { get; set; }
    }    

    public class NetRecord
    {
        public string name { get; set; }
        public string attributesIdString { get; set; }
        public uint index { get; set; }
        public SubnetRecord[] subnetRecords { get; set; }
    }

    public class SubnetRecord
    {
        public string type { get; set; }
        public FeatureIdRecord[] featureIdRecords { get; set; }
        public BoardSide side { get; set; }
        public uint componentNumber { get; set; }
        public uint toeprintNumber { get; set; }
    }

    public class FeatureIdRecord
    {
        public string type { get; set; }
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
    }

    public class PinRecord
    {
        public string name { get; set; }
        public string type { get; set; }
        public float xCenter { get; set; }
        public float yCenter { get; set; }
        public int finishedHoleSize { get; set; }
        public string electricalType { get; set; }
        public string mountType { get; set; }
        public uint id { get; set; }
        public uint index { get; set; }
    }

    public class OutlineRecord
    {
        public string type { get; set; }
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
        public uint row { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public long cuTop { get; set; }
        public long cuBottom { get; set; }
        public long _ref { get; set; }
        public uint id { get; set; }
        public string startName { get; set; }
        public string endName { get; set; }
        public string context { get; set; }
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
        public string polarity { get; set; }
        public string shape { get; set; }
        public float width { get; set; }
    }

    public class SymbolsDirectory
    {
        public string name { get; set; }
        public string path { get; set; }
        public AttrListFile attrlistFile { get; set; }
        public FeaturesFile featureFile { get; set; }
    }
}
