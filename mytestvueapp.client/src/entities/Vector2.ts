export class Vector2 {
  x: number;
  y: number;

  constructor(X: number, Y: number) {
    this.x = X;
    this.y = Y;
  }

  static VectorBetween(v1: Vector2, v2: Vector2): Vector2 {
    return new Vector2(v1.x - v2.x, v1.y - v2.y);
  }
}
