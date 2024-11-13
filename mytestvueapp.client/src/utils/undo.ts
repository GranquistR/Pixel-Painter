



class LinkedNode {
    private _elem: string[][];
    public next: LinkedNode | null;
   
    constructor(elem: string[][])  {
        this._elem = elem;
        this.next = null;
        
    }

    get elem(): string[][] {
        return this._elem;
    }
}

export default class LinkedList<T> {
    private head: LinkedNode | null = null;
    private tail: LinkedNode | null = null;
    private current: LinkedNode | null = null;

    private len = 0;

    constructor(headElement?: LinkedNode, tailElement?: LinkedNode,currentElement?: LinkedNode) {
        this.head = headElement || null;
        this.tail = tailElement || null;
        this.current = currentElement || null;
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
            this.tail = node;
            this.current = node;
            }
        }
        this.len++;
        console.log(this.len);
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
            
            console.log(isDifferent);
            

        }
        if (isDifferent){
            this.append(pixelGrid);
            
        }
       

       
    }

    public getPrevious(){
        
         if (!this.head) {
            return null; // Empty list
          }
          let previous = this.head;
          if(previous === this.current){
            return this.head.elem;
          }
          while (previous.next && previous.next.next && previous.next != this.current) {
            previous = previous.next;
          }
         

          this.current = previous;
        return this.current.elem;
          
           
        }

    public getNext(){
        if(this.current && this.current.next){
            this.current = this.current.next
        return this.current.elem;
         }
        }
    
        

       
   

    
  
}