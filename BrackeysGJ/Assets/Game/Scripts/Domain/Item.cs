using System;
using UnityEngine;

public class Item
{
    private Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Sprite Image { get; set; }

    public Item()
    {
        Id = Guid.Empty;
        Name = string.Empty;
        Description = string.Empty;
        Image = null;
    }

    public Item(string name, string description, Sprite image) : this()
    {
        SetName(name);
        SetDescription(description);
        SetImage(image);
    }

    private void SetName(string name)
    {
        if (name == null)
            throw new InvalidOperationException("Name can't be null");

        Name = name;
    }

    private void SetDescription(string description)
    {
        if (description == null)
            throw new InvalidOperationException("Description can't be null");

        Description = description;
    }

    private void SetImage(Sprite image)
    {
        if (image == null)
            throw new InvalidOperationException("Image can't be null");

        Image = image;
    }

    public override bool Equals(object obj)
    {
        return ((Item)obj).Id == Id;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
