
package tanksmash;

public class GameEngine {
    
    private boolean gameStarted;
    private boolean gameFinished;
    private boolean alive;
    
    public GameEngine(){
        this.gameStarted = false;
        this.gameFinished = false;
        this.alive = false;
    }
    
    public void decode(String message){
        
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
