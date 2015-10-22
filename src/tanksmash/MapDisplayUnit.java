
package tanksmash;

import java.awt.Canvas;
import java.awt.Color;
import java.awt.Graphics2D;
import java.awt.Toolkit;

public class MapDisplayUnit extends Canvas{

    public MapDisplayUnit() {
        this.setIgnoreRepaint(true);
    }

    public void draw() {
        Graphics2D g = (Graphics2D) this.getGraphics();
        
        Toolkit.getDefaultToolkit().sync();
        g.dispose();
    }
    
}
