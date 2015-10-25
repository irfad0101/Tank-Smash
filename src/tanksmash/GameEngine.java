
package tanksmash;

import entity.*;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import javax.swing.JOptionPane;

public class GameEngine {
    
    private boolean gameStarted;
    private boolean gameFinished;
    private boolean alive;
    private String myTank;
    private ArrayList<Brick> brickList;
    private ArrayList<Stone> stoneList;
    private ArrayList<WaterPit> waterList;
    private ArrayList<Bullet> bulletList;
    private ArrayList<Tank> tankList;
    private GameWindow gameWindow;
    private MapDisplayUnit[][] mapDisplay;
    
    public GameEngine(GameWindow gameWindow,MapDisplayUnit[][] mapDisplay){
        this.gameStarted = false;
        this.gameFinished = false;
        this.alive = false;
        brickList = new ArrayList<Brick>();
        stoneList = new ArrayList<Stone>();
        waterList = new ArrayList<WaterPit>();
        bulletList = new ArrayList<Bullet>();
        tankList = new ArrayList<Tank>();
        this.gameWindow = gameWindow;
        this.mapDisplay = mapDisplay;
    }
    
    public synchronized void decode(String message){
        switch(message){
            // common messages
            case Command.REQUESTERROR:
                gameWindow.showStatus("Request Error");
                break;
            case Command.SERVERERROR:
                gameWindow.showStatus("Server Error");
                JOptionPane.showMessageDialog(gameWindow, "Oops! an error has occured in server.", "Server Error", JOptionPane.ERROR_MESSAGE);
                break;
            // messages recieved when cannot join game
            case Command.ALREADYADDED:
                gameWindow.showStatus("You has already joined the game");
                break;
            case Command.ALREADYSTARTED:
                gameWindow.showStatus("Game has started. Try agiain later.");
                JOptionPane.showMessageDialog(gameWindow, "The Game has already started. Please join to a new game later.", "Game Already Started", JOptionPane.INFORMATION_MESSAGE);
                break;
            case Command.PLAYERFULL:
                gameWindow.showStatus("Player limit reached. Try again later.");
                JOptionPane.showMessageDialog(gameWindow, "The maximum no of allowed players has joined.Please join the next new game","Player Full",JOptionPane.INFORMATION_MESSAGE);
                break;
            // responses for moveing
            case Command.CELLOCCUPIED:
                gameWindow.showStatus("Invalid move. Cell already is occupied");
                break;
            case Command.TOOQUICK:
                gameWindow.showStatus("Too quick for a move");
                break;
            case Command.INVALIDCELL:
                gameWindow.showStatus("The cell you try to move is invalid.");
                break;
            case Command.NOTYETSTARTED:
                gameWindow.showStatus("Game not yet started to send command.");
                break;
            case Command.ALREADYFINISH:
                gameWindow.showStatus("Cannot play further. Game has finished.");
                break;
            case Command.NOTACONTESTANT:
                gameWindow.showStatus("Not allowed. You have not joined the game.");
                break;
            case Command.DEAD:
                gameWindow.showStatus("You tank is smashed");
                break;
            default:
                // handle commands that need some string manupilation
                if (message.startsWith(Command.OBSTACLE)){
                    gameWindow.showStatus("You hit an obstacle. Penalty: "+message.substring(9, message.length()-1));
                }
                else if (message.startsWith("I")){
                    initializeMap(message.substring(2, message.length()-1));
                }
                else if(message.startsWith("S")){
                
                }
                else if(message.startsWith("L")){
                
                }
                else if(message.startsWith("C")){
                
                }
                else if(message.startsWith("G")){
                
                }
        }
    }
    
    private void initializeMap(String details){
        try{
        /* locations of my player name,bricks, stones and water in the string respectively. They are seperated with a colon. elements array will have those 
           seperated but as strings. */
        String[] elements = details.split(":");
        myTank = elements[0];  // my player name. this is the first value in elements array
        /* process brick locations. elements[1] has brick locations each brick seperated with semi-colon and cordinates seperated with commas. bricks array
           will have x,y cordinates of bricks as string*/
        String[] bricks = elements[1].split("[;,]");
        for (int i=0; i<bricks.length; i+=2){
            Brick brick = new Brick(Integer.parseInt(bricks[i]), Integer.parseInt(bricks[i+1]), 0);
            brickList.add(brick);
            mapDisplay[brick.getX()][brick.getY()].setGameObject(brick);
            mapDisplay[brick.getX()][brick.getY()].draw();
        }
        String[] stones = elements[2].split("[;,]");
        String[] water = elements[3].split("[;,]");
    
        }catch(IOException e){
            System.out.println("IOException while loading image for map.");
            JOptionPane.showMessageDialog(gameWindow, "An error occured while loading map. Cannot find an image","IOException",JOptionPane.ERROR_MESSAGE);
        }
    }
    
    public boolean isGameStarted(){
        return gameStarted;
    }
    
    public void setGameStarted(boolean gameStarted){
        this.gameStarted = gameStarted;
    }
    
    public boolean isGameFinished(){
        return gameFinished;
    }
    
    public void setGameFinsished(boolean gameFinished){
        this.gameFinished = gameFinished;
    }
    
    public boolean isAlive(){
        return alive;
    }
    
    public void setAlive(boolean alive){
        this.alive = alive;
    }
    
}
