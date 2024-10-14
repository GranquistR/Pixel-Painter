export default class PainterTool {
  label: string;
  icon: string;
  shortcut: string;
  cursor: string;

  constructor(label: string, icon: string, shortcut: string, cursor: string) {
    this.label = label;
    this.icon = icon;
    this.shortcut = shortcut;
    this.cursor = cursor;
  }

  static getDefaults(): PainterTool[] {
    return [
      new PainterTool("Pan", "pi pi-arrows-alt", "p", "grab"),
      new PainterTool("Brush", "pi pi-pencil", "b", "crosshair"),
      new PainterTool("Eraser", "pi pi-eraser", "e", "crosshair"),
      new PainterTool("Pipette","pi pi-eye dropper","d","crosshair"),
      new PainterTool("Paint-Bucket", "pi pi-bucket", "f", "crosshair")
    ];
  }
}
