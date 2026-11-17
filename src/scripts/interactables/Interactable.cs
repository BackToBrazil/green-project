using Godot;
using System;
using System.Threading.Tasks;
namespace GreenProject.Interactable;

public interface Interactable
{
    public Task Interact();
}
