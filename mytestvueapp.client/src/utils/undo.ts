



class LinkedNode<T> {
    private _elem: T[][];
    public next: LinkedNode<T> | null;
   
    constructor(elem: T[][])  {
        this._elem = elem;
        this.next = null;
        
    }

    get elem(): T[][] {
        return this._elem;
    }
}

export default class LinkedList<T> {
    private head: LinkedNode<T> | null = null;

    private len = 0;

    constructor(headElement?: LinkedNode<T>) {
        this.head = headElement || null;

    }

    public append(elem: T[][]) {
        const node = new LinkedNode(elem);
        let current: LinkedNode<T>;

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

    public isDifferent(pixelGrid:T[][]){
        let isDifferent = false;
        const last = this.getLast();
        if(last!==null && this.head!==null){
        for (let i = 0; i < pixelGrid.length; i++) {
            for (let j = 0; j < pixelGrid.length; j++) {
                if(pixelGrid[i][j] !== last[i][j]){
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

    getLast(): T[][] | null {
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