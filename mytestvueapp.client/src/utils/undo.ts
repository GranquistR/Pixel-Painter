



class LinkedNode {
    private _elem: string[][];
    public next: LinkedNode | null;
    public prev: LinkedNode | null;
   

    constructor(elem: string[][])  {
        this._elem = elem;
        this.next = null;
        this.prev = null;
    }

    get elem(): string[][] {
        return this._elem;
    }
}

export default class LinkedList {
    private head: LinkedNode | null = null;
    private tail: LinkedNode | null = null;
    private current: LinkedNode | null = null;
    public size :number;

    constructor(headElement?: LinkedNode, tailElement?: LinkedNode,currentElement?: LinkedNode) {
        this.head = headElement || null;
        this.tail = tailElement || null;
        this.current = currentElement || null;
        this.size = 0; 
    }

    public append(pixelGrid: string[][]) {
        const node = new LinkedNode(pixelGrid);

        if (this.head === null && this.current === null && this.tail === null) {
            this.head = node;
            this.tail = node;
            this.current = node;
        } else {
            if(this.current){
           this.current.next=node;
           node.prev = this.current;
            this.tail = node;
            this.current = node;
            }
        }
       
        this.size++;
        if (this.size ===301 && this.head ){
            this.head = this.head.next
            if(this.head){
            this.head.prev=null;
            }
            this.size--;
        }
    }

    public isDifferent(pixelGrid:string[][]){
        let isDifferent = false;
        
        if(this.current!==null && this.head!==null){
        for (let i = 0; i < pixelGrid.length; i++) {
            for (let j = 0; j < pixelGrid.length; j++) {
                if(pixelGrid[i][j] !== this.current.elem[i][j]){
                    isDifferent=true;
                }
            }
          }            

        }
        return isDifferent;
    }

    public getPrevious(){
        
         if (!this.head) {
            return null; // Empty list
          }
          if(this.current && this.current.prev){
            this.current = this.current.prev;
            this.size--;
            return this.current.elem;
          }
           
        }

    public getNext(){
        if(this.current && this.current.next){
            this.current = this.current.next
            this.size++;
            return this.current.elem;
         }
        }
    
    public linkedListToArray(){
        const array: string[][][]=[];
        let iterator = this.head;
        while(iterator && iterator.next){
            array.push(iterator.elem);
            iterator = iterator.next;
        }
        if(iterator){
        array.push(iterator.elem);
        }
        return array;

    }
    public arrayToLinkedList(array: string[][][]){
        const list = new LinkedList();
        for(let i=0; i < array.length; i++){
            list.append(array[i]);
        }
        return list;
    }
    public updateCurrent(pixelGrid: string[][]){
       this.current = this.head;
       
       while(this.isDifferent(pixelGrid) && this.current){
        this.current= this.current.next;
       }
       
    }
}