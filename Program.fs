namespace OrderTaking.Domain
  // These wrapped types lead to a little bit of overhead, but for business
  // applications there is no problem. Beware with high performance requirements,
  // though
  type WidgetCode = WidgetCode of string
  type GizmoCode = GizmoCode of string

  [<Struct>] // Makes this type cache-friendly when stored in arrays
  type UnitQuantity = UnitQuantity of int
  type KilogramQuantity = KilogramQuantity of decimal

  type OrderQuantity =
    | Unit of UnitQuantity
    | Kilogram of KilogramQuantity

  type Undefined = exn

  type CustomerInfo = Undefined
  type ShippingAddress = Undefined
  type BillingAddress = Undefined
  type BillingAmount = Undefined
  type Price = Undefined

  type OrderId = Undefined
  type CustomerId = Undefined
  type OrderLineId = Undefined

  type ProductCode =
    | Widget of WidgetCode
    | Gizmo of GizmoCode

  type Order = {
    Id : OrderId
    CustomerInfo : CustomerInfo
    ShippingAddress: ShippingAddress
    BillingAddress: BillingAddress
    OrderLines: OrderLine list
    AmountToBill: BillingAmount
  }
  and OrderLine = {
    Id : OrderLineId
    OrderId : OrderId
    ProductCode : ProductCode
    OrderQuantity : OrderQuantity
    Price : Price
  }

  type UnvalidatedOrder = {
    OrderId : string
    CustomerInfo : CustomerInfo
    ShippingAddress : ShippingAddress
  }

  type PlaceOrderEvents = {
    AcknowledgementSent : Undefined
    OrderPlaced : Undefined
    BillableOrderPlaced : Undefined
  }

  type PlaceOrderError =
    | ValidationError of ValidationError list
    // other errors
  and ValidationError = {
    FieldName : string
    ErrorDescription : string
  }

  type PlaceOrder = UnvalidatedOrder -> Result<PlaceOrderEvents, PlaceOrderError>
