import type { PixelGrid } from "@/entities/PixelGrid";




class LinkedNode<PixelGrid> {
    private _elem: PixelGrid;
    public next: LinkedNode<PixelGrid> | null;
   
    constructor(elem: PixelGrid)  {
        this._elem = elem;
        this.next = null;
        
    }

    get elem(): PixelGrid {
        return this._elem;
    }
}

export default class LinkedList<PixelGrid> {
    private head: LinkedNode<PixelGrid> | null = null;

    private len = 0;

    constructor(headElement?: LinkedNode<PixelGrid>) {
        this.head = headElement || null;
    }

    public append(elem: PixelGrid) {
        const node = new LinkedNode(elem);
        let current: LinkedNode<PixelGrid>;

        if (this.head === null) {
            this.head = node;
        } else {
            current = this.head;
            while (current.next) {
                current = current.next;
            }
            current.next = node;
        }
        this.len++;
        console.log(this.len);
    }

    public isDifferent(pixelGrid:PixelGrid){
        if(pixelGrid === this.getLast()){
            this.append(pixelGrid);
        }
       
    }

    getLast(): PixelGrid | null {
        if (!this.head) {
          return null; // Empty list
        }
    
        let current = this.head;
        while (current.next) {
          current = current.next;
        }
    
        return current.elem;
      }
  
}