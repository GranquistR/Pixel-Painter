import type { PixelGrid } from "@/entities/PixelGrid";




class LinkedNode<PixelGrid> {
    private _elem: PixelGrid;
    public next: LinkedNode<PixelGrid> | null;

    constructor(elem: PixelGrid) {
        this._elem = elem;
        this.next = null;
    }

    get elem(): PixelGrid {
        return this._elem;
    }
}

export default class LinkedList<PixelGrid> {
    private head: LinkedNode<PixelGrid> | null = null;
    private tail: LinkedNode<PixelGrid> | null = null;

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
        this.tail=node;
        this.len++;
        console.log('gotcalled');
    }

    public removeAt(pos: number): LinkedNode<PixelGrid> | null {
        if (pos > -1 && pos < this.len && this.head) {
            let current = this.head;
            let previous: LinkedNode<PixelGrid> = current;
            let index = 0;

            if (pos === 0) {
                this.head = current.next;
            } else {
                while (index++ < pos && current.next) {
                    previous = current;
                    current = current.next;
                }
                previous.next = current.next;
            }
            this.len--;
            return current;
        } else {
            return null;
        }
    }


    public insert(elem: PixelGrid, pos: number) {
        if (pos > -1 && pos < this.len && this.head) {
            let current = this.head;
            let index = 0;
            let previous = current;
            const node = new LinkedNode(elem);

            if (pos === 0) {
                node.next = current;
                this.head = node;
            } else {
                while (index++ < pos && current.next) {
                    previous = current;
                    current = current.next;
                }
                node.next = current;
                previous.next = node;
            }
            this.len++;
            return true;
        } else {
            return false;
        }
    }
    public toString() {
        let current = this.head;
        let str = '';
        while (current) {
            str += current.elem;
            current = current.next;
        }
        return str;
    }
    public isDifferent(PixelGrid:PixelGrid){
        if(PixelGrid === this.tail){
            this.append(PixelGrid);
        }
    }
  
}