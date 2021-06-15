using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path {

	public string type;
    public int x;
    public int y;

    public Path(int x, int y, string type)
    {
        this.x = x;
        this.y = y;
        this.type = type;
    }

}
