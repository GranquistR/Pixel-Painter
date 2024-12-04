export default class CustomColor {
    hex: string;
    shortcut: string;
    
  
    constructor(hex: string, shortcut: string) {
      this.hex = hex;
      this.shortcut = shortcut;
    }
  
    static getCustomColors(): CustomColor[] {
      return [
        new CustomColor("", "Shortcut: 1",),
        new CustomColor("", "Shortcut: 2",),
        new CustomColor("", "Shortcut: 3",),
        new CustomColor("", "Shortcut: 4",),
        new CustomColor("", "Shortcut: 5",),
        new CustomColor("", "Shortcut: 6",),
        new CustomColor("", "Shortcut: 7",),
        new CustomColor("", "Shortcut: 8",),
        new CustomColor("", "Shortcut: 9",),
        new CustomColor("", "Shortcut: 0",),
        new CustomColor("", "Shortcut: -",),
        new CustomColor("", "Shortcut: =",)

        
      ];
    }
    
    
  }