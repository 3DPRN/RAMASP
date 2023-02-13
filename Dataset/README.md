# Dataset
The file contains an example of dataset exported from the system as NGSI v2 entities.

## Workstation entity
**id:** univocal id for workstation entity

**type:** type of entity

**3DPrintedPiecesCheckedOK:** number of pieces completed and checked on the system

**3DPrintedPiecesCompleted:** number of pieces completed on the system

**3DPrintedPiecesStarted:** number of pieces started on the system

**3DPrinterProductiveCount:** number of productive 3D printers (Ready or printing)

**3DPrintersTotal:** total number of 3D printers installed on the system

**I40AssetName:** Name of the entity

**I40PhysicalModelType:** Type of entity

**I40Timespamp:** Timestamp for the event ("yyyy-MM-dd_HH:mm:ss.fff”)

**Oee:** current calculated Overall Equipment Effectiveness

**OeeAvailability:** oee availability value of the system

**OeePerformance:** oee performance value of the system

**OeeQuality:** oee quality value of the system


## Unit entity (printer)
**id:** univocal id for unit entity

**type:** type of entity

**BedCurrentTemp:** current temperature of the heated bed of the unit

**BedTargetTemp:** target temperature of the heated bed of the unit

**ExtruderCurrentTemp:** current temperature of the extruder of the unit

**ExtruderTargetTemp:** target temperature of the extruder of the unit

**I40AssetName:** Name of the entity

**I40PhysicalModelType:** Type of entity

**I40Timespamp:** Timestamp for the event ("yyyy-MM-dd_HH:mm:ss.fff”)

**Status:** status of the unit (Unknown, StandBy, Printing, PrintingPause, Mantainance, Error)

**hasParentI40Asset:** name of the parent entity
