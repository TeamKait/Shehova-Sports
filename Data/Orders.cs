//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sports.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Orders
    {
        public int Id { get; set; }
        public System.DateTime OrderDate { get; set; }
        public System.DateTime DeliveryDate { get; set; }
        public int PickPointId { get; set; }
        public int UserId { get; set; }
        public int Code { get; set; }
        public int OrderStatusId { get; set; }
        public int ProductId { get; set; }
    
        public virtual OrderStatuses OrderStatuses { get; set; }
        public virtual Product Product { get; set; }
        public virtual Users Users { get; set; }
    }
}
