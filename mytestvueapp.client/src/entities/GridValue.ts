export default class GridValue {
  hex: string;
  alpha: number;

  constructor(hex_:string, alpha_?: number) {
    this.hex = hex_;

    if (alpha_)
      this.alpha = alpha_;
    else
      this.alpha = 0;
  }
}